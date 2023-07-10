using Discovery.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.Configure<ConsulOptions>(builder.Configuration.GetSection(ConsulOptions.Section));

var app = builder.Build();
app.Run();