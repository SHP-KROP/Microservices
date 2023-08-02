using AuctionService.Application.Options;
using AuctionService.Application.Services.Abstractions.Repositories;
using AuctionService.Infrastructure.Messaging;
using AuctionService.Infrastructure.Messaging.Contracts;
using Microsoft.Extensions.Options;

namespace AuctionService.HostedServices;

public sealed class AuctionBeginningMonitor : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<AuctionBeginningMonitor> _logger;
    private readonly AuctionMonitorOptions _auctionMonitorOptions;

    private Timer _timer;
    private DateTimeOffset _latestActivation;
    
    public AuctionBeginningMonitor(
        IServiceScopeFactory serviceScopeFactory, 
        ILogger<AuctionBeginningMonitor> logger,
        IOptions<AuctionMonitorOptions> auctionMonitorOptions
        )
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _auctionMonitorOptions = auctionMonitorOptions.Value;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started monitoring auctions which are ready to be started");
        _latestActivation = DateTimeOffset.Now;
        
        _timer = new Timer(async (obj) => await PublishReadyToStartAuctions(obj), 
            null, TimeSpan.Zero, TimeSpan.FromSeconds(_auctionMonitorOptions.MonitoringIntervalSeconds));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        _logger.LogInformation("Finished monitoring auctions which are ready to be started");

        return Task.CompletedTask;
    }
    
    private async Task PublishReadyToStartAuctions(object obj)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        
        var auctionRepository = scope.ServiceProvider.GetRequiredService<IAuctionRepository>();
        
        _logger.LogInformation("Looking for auctions which are ready to be started");
        var startCandidates = await auctionRepository.GetReadyToStartAuctionsSince(_latestActivation);
        
        if (!startCandidates.Any())
        {
            _logger.LogInformation("Auctions which are ready to be started were not found");
            return;
        }
        
        var message = new ReadyToStartAuctionsMessage();
        message.AddRange(startCandidates);
        
        var publisher = scope.ServiceProvider.GetRequiredService<IProducer<ReadyToStartAuctionsMessage>>();
        
        _logger.LogInformation("Auctions which are ready to be started found - publishing event with auction ids");
        await publisher.Publish(message);
    }
}