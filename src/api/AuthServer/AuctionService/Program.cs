using AuctionService.Hubs;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceRegistration.Extensions;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDiscovery(builder.Configuration);
services.AddSignalR();

builder.Host.UseSerilog((context, configuration) 
    => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseSerilogRequestLogging();

app.MapGet("/api/auction", ([FromServices] ILogger logger) =>
{
    logger.Information("ASDF!!!!");

    return new ObjectResult("AUCTION!!!");
});

app.MapHub<AuctionHub>("messaging-auction");

app.Run();