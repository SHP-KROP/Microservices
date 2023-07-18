using AuctionService.Application.Models.Auction.Validators;
using AuctionService.Extensions;
using AuctionService.Hubs;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using ServiceRegistration.Extensions;

var builder = WebApplication.CreateBuilder(args);
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

services.AddDiscovery(builder.Configuration);
services.AddSignalR();
services.AddControllers();

services.AddPersistenceServices(builder.Configuration);
services.AddBusinessLogicServices();

services.AddMvc();
services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssembly(typeof(AuctionCreateModelValidator).Assembly);

builder.Host.UseSerilog((context, configuration) 
    => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseCors("DefaultPolicy");

app.UseSerilogRequestLogging();

app.MapControllers();

app.MapHub<AuctionHub>("messaging-auction");

app.Run();