using ServiceRegistration.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDiscovery(builder.Configuration);

var app = builder.Build();
app.MapGet("/api/auction", () => "Auction");

app.Run();