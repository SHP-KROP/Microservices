using Consul;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceRegistration.Abstractions;
using ServiceRegistration.Options;

namespace ServiceRegistration;

internal sealed class ConsulRegisterService : IConsulRegisterService
{
    private readonly IConsulClient _consulClient;
    private readonly ILogger<ConsulRegisterService> _logger;
    private readonly DiscoveryOptions _discoveryOptions;

    public ConsulRegisterService(
        IConsulClient consulClient, 
        IOptions<DiscoveryOptions> discoveryOptions,
        ILogger<ConsulRegisterService> logger)
    {
        _consulClient = consulClient;
        _discoveryOptions = discoveryOptions.Value;
        _logger = logger;
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
        
        _logger.LogInformation("Succesfully registered service {@Service} to consul", serviceRegistration);
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