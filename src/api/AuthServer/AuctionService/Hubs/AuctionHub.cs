using AuctionService.Application.Events;
using AuctionService.Application.Services.Abstractions;
using AuctionService.Core.Entities;
using AuctionService.Infrastructure.Messaging.Constants;
using AuctionService.Infrastructure.Messaging.Contracts;
using AuctionService.Infrastructure.Messaging.Extensions;
using AuctionService.Infrastructure.Messaging.Options;
using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace AuctionService.Hubs;

internal sealed class AuctionHub : Hub, IHostedService
{
    private readonly ILogger<AuctionHub> _logger;
    private readonly IAuctionHost _auctionHost;
    private readonly IAuctionService _auctionService;
    private readonly KafkaOptions _kafkaOptions;

    public AuctionHub(
        ILogger<AuctionHub> logger,
        IAuctionHost auctionHost,
        IOptions<KafkaOptions> kafkaOptions,
        IAuctionService auctionService)
    {
        _logger = logger;
        _auctionHost = auctionHost;
        _kafkaOptions = kafkaOptions.Value;
        _auctionService = auctionService;
    }

    #region AuctionHub

    public async Task UpdateBid(BidUpdatedEvent bidUpdatedEvent, string auctionId)
    {
        string userId = Context.UserIdentifier;

        await _auctionService.CreateBid(bidUpdatedEvent, userId);

        Clients.Group(auctionId).SendAsync("BidUpdated", bidUpdatedEvent);

        //return Task.CompletedTask;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("Client {@ClientId} connected to Auction hub", Context.ConnectionId);
        
        return base.OnConnectedAsync();
    }

    #endregion

    public async Task Consume()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _kafkaOptions.BootstrapServers,
            GroupId = Guid.NewGuid().ToString(),
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        _logger.LogInformation("Started listening to {@BootstrapServers}", _kafkaOptions.BootstrapServers);

        consumer.Subscribe(TopicNames.ReadyToStartAuctionsTopic);
        _logger.LogInformation("Subscribed to topic {@Topic}", TopicNames.ReadyToStartAuctionsTopic);

        var cts = new CancellationTokenSource();

        while (true)
        {
            try
            {
                var auctionIds = consumer.ConsumeDeserializedMessage<ReadyToStartAuctionsMessage>(cts);
                _logger.LogTrace("Consumed {@ReadyToStartAuctionMessage}", auctionIds);

                var auctionsToBeStarted = (await _auctionHost.GetActiveAuctions()).Keys.Except(auctionIds);

                if (!auctionsToBeStarted.Any())
                {
                    continue;
                }
                
                await _auctionHost.StartAuctions(auctionsToBeStarted);
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
            catch (ConsumeException ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }

    #region IHostedService

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () => await Consume(), cancellationToken);
        
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Dispose();
    }

    #endregion
}