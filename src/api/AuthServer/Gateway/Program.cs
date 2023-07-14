using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
    
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("ocelot.Development.json", optional: true, reloadOnChange: true);
}

builder.Configuration.AddEnvironmentVariables();

var services = builder.Services;

services.AddCors(x =>
{
    x.AddPolicy("DefaultPolicy",
        options => options
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

services.AddOcelot(builder.Configuration)
    .AddConsul();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseCors("DefaultPolicy");

app.UseRouting();
app.UseWebSockets();
await app.UseOcelot();
app.UseSerilogRequestLogging();

app.Run();