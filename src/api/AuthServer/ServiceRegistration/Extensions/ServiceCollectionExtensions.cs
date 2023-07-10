using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceRegistration.Abstractions;
using ServiceRegistration.Options;

namespace ServiceRegistration.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscovery(this IServiceCollection @this, IConfiguration configuration)
    {
        var consulSection = configuration.GetSection(ConsulOptions.Section);
        
        @this.Configure<ConsulOptions>(consulSection);
        @this.AddSingleton<IConsulClient, ConsulClient>(_ => 
            new ConsulClient(config => config.Address = new Uri(consulSection["Url"])));
        
        @this.Configure<DiscoveryOptions>(configuration.GetSection(DiscoveryOptions.Section));
        
        @this.AddHostedService<ConsulRegisterService>();

        return @this;
    }
}