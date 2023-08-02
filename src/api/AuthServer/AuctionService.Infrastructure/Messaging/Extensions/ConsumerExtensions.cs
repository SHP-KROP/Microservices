using Confluent.Kafka;
using Newtonsoft.Json;

namespace AuctionService.Infrastructure.Messaging.Extensions;

public static class ConsumerExtensions
{
    public static TMessage? ConsumeDeserializedMessage<TMessage>(
        this IConsumer<Ignore, string> consumer, CancellationTokenSource cts)
    {
        var message = consumer.Consume(cts.Token);
        
        return JsonConvert.DeserializeObject<TMessage>(message.Message.Value);
    }
}