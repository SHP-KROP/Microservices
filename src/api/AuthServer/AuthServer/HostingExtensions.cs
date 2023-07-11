using Duende.IdentityServer;
using AuthServer;
using AuthServer.Pages.Admin.ApiScopes;
using AuthServer.Pages.Admin.Clients;
using AuthServer.Pages.Admin.IdentityScopes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.AspNetCore.Identity;
using AuthServer.Database;

namespace AuthServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        var connectionString = configuration.GetConnectionString("YehorConnection");

        var migrationAssembly = typeof(Config).Assembly.GetName().Name;
        
        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationAssembly));
        });

        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        //var isBuilder = 
        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v5/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            // this adds the config data from DB (clients, resources, CORS)
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString,
                        dbOpts => dbOpts.MigrationsAssembly(migrationAssembly));
            })
            // this is something you will want in production to reduce load on and requests to the DB
            //.AddConfigurationStoreCache()
            //
            // this adds the operational data from DB (codes, tokens, consents)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString,
                        dbOpts => dbOpts.MigrationsAssembly(migrationAssembly));
                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
                options.RemoveConsumedTokens = true;
            })
            .AddAspNetIdentity<IdentityUser>();

        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5001/signin-google
                options.ClientId = "copy client ID from Google here";
                options.ClientSecret = "copy client secret from Google here";
            });


        //// this adds the necessary config for the simple admin/config pages
        //{
        //    builder.Services.AddAuthorization(options =>
        //        options.AddPolicy("admin",
        //            policy => policy.RequireClaim("sub", "1"))
        //    );

        //    builder.Services.Configure<RazorPagesOptions>(options =>
        //        options.Conventions.AuthorizeFolder("/Admin", "admin"));

        //    builder.Services.AddTransient<ClientRepository>();
        //    builder.Services.AddTransient<IdentityScopeRepository>();
        //    builder.Services.AddTransient<ApiScopeRepository>();
        //}

        //// if you want to use server-side sessions: https://blog.duendesoftware.com/posts/20220406_session_management/
        //// then enable it
        ////isBuilder.AddServerSideSessions();
        ////
        //// and put some authorization on the admin/management pages using the same policy created above
        ////builder.Services.Configure<RazorPagesOptions>(options =>
        ////    options.Conventions.AuthorizeFolder("/ServerSideSessions", "admin"));

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
        app.UseAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}