using Consul;
using Microsoft.Extensions.Options;
using ServiceRegistration.Abstractions;
using ServiceRegistration.Options;

namespace ServiceRegistration;

internal sealed class ConsulRegisterService : IConsulRegisterService
{
    private readonly IConsulClient _consulClient;
    private readonly DiscoveryOptions _discoveryOptions;

    public ConsulRegisterService(IConsulClient consulClient, IOptions<DiscoveryOptions> discoveryOptions)
    {
        _consulClient = consulClient;
        _discoveryOptions = discoveryOptions.Value;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var uri = new Uri(_discoveryOptions.Url);

        var serviceRegistration = new AgentServiceRegistration
        {
            Address = uri.Host,
            Name = _discoveryOptions.ServiceName,
            Port = uri.Port,
            ID = _discoveryOptions.ServiceId,
            Tags = new[] { _discoveryOptions.ServiceName }
        };
        
        await _consulClient.Agent.ServiceDeregister(_discoveryOptions.ServiceId, cancellationToken);
        await _consulClient.Agent.ServiceRegister(serviceRegistration, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _consulClient.Agent.ServiceDeregister(_discoveryOptions.ServiceId, cancellationToken);
        }
        catch (Exception ex)
        {
            // TODO: Log exception here
        }
    }
}