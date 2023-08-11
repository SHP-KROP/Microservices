using AuctionService.Application.Models.Auction.Validators;
using AuctionService.Application.Services;
using AuctionService.Application.Services.Abstractions;
using AuctionService.Extensions;
using AuctionService.Hubs;
using AuctionService.Infrastructure.Persistence;
using Authentication.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
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

services.AddIdentityServerAuthentication(builder.Configuration);

services.AddDiscovery(builder.Configuration);
services.AddSingleton<IAuctionHubService, AuctionHubService>();
services.AddSignalR();
services.AddControllers();

services.AddInfrastructureServices(builder.Configuration);
services.AddPersistenceServices(builder.Configuration);
services.AddBusinessLogicServices(builder.Configuration);

services.AddMvc();
services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssembly(typeof(AuctionCreateModelValidator).Assembly);

builder.Host.UseSerilog((context, configuration) 
    => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.Services.CreateScope().ServiceProvider.GetRequiredService<AuctionDbContext>().Database.Migrate();

if (app.Environment.IsDevelopment() && args.Contains("/seed"))
{
    await app.SeedData();
    return;
}

app.UseCors("DefaultPolicy");
app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<AuctionHub>("messaging-auction");

app.Run();