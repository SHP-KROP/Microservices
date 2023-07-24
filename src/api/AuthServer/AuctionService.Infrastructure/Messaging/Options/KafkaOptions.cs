namespace AuctionService.Infrastructure.Messaging.Options;

public sealed class KafkaOptions
{
    public const string Section = "Kafka";
    
    public string BootstrapServers { get; init; }
}