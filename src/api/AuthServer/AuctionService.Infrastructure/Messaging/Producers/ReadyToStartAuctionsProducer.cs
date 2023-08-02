using System.Net;
using AuctionService.Infrastructure.Messaging.Constants;
using AuctionService.Infrastructure.Messaging.Contracts;
using AuctionService.Infrastructure.Messaging.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AuctionService.Infrastructure.Messaging.Producers;

public sealed class ReadyToStartAuctionsProducer : IProducer<ReadyToStartAuctionsMessage>
{
    private readonly ILogger<ReadyToStartAuctionsProducer> _logger;
    private readonly KafkaOptions _kafkaOptions;

    public ReadyToStartAuctionsProducer(ILogger<ReadyToStartAuctionsProducer> logger, IOptions<KafkaOptions> kafkaOptions)
    {
        _logger = logger;
        _kafkaOptions = kafkaOptions.Value;
    }

    public async Task Publish(ReadyToStartAuctionsMessage message)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = _kafkaOptions.BootstrapServers,
            ClientId = Dns.GetHostName()
        };

        try
        {
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            await producer.ProduceAsync(
                TopicNames.ReadyToStartAuctionsTopic, new Message<Null, string>
                {
                    Value = JsonConvert.SerializeObject(message)
                });
        }
        catch (ProduceException<Null, string> ex)
        {
            _logger.LogError("Delivery failed: {@ErrorReason}", ex.Error.Reason);
        }
    }
}