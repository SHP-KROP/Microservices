using AuthServer.Configuration;
using AuthServer.Database;
using AuthServer.Mapping;
using AuthServer.Services;
using AuthServer.Services.Interfaces;
using AuthServer.Valdators;
using Duende.IdentityServer;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ServiceRegistration.Extensions;

namespace AuthServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var migrationAssembly = typeof(Config).Assembly.GetName().Name;
        
        builder.Services.AddRazorPages();
        builder.Services.AddDiscovery(configuration);

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationAssembly));
        });

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.EmitStaticAudienceClaim = true;
            })
            // this adds the config data from DB (clients, resources, CORS)
            .AddConfigurationStore(options =>
            {
                options.DefaultSchema = "auth";
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString,
                        dbOpts => dbOpts.MigrationsAssembly(migrationAssembly));
            })
            // this adds the operational data from DB (codes, tokens, consents)
            .AddOperationalStore(options =>
            {
                options.DefaultSchema = "auth";
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString,
                        dbOpts => dbOpts.MigrationsAssembly(migrationAssembly));
                options.EnableTokenCleanup = true;
                options.RemoveConsumedTokens = true;
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddJwtBearerClientAuthentication();

        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                options.ClientId = "copy client ID from Google here";
                options.ClientSecret = "copy client secret from Google here";
            });

        builder.Services.AddAutoMapper(typeof(AuthMappingProfile));

        builder.Services.AddControllers();

        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        builder.Services.AddFluentValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<AuthServer.Common.IAssemblyMarker>(filter: x => x.ValidatorType != typeof(PasswordValidator));

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseIdentityServer();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}