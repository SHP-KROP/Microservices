namespace AuctionService.Infrastructure.Messaging;

public interface IProducer<T>
{
    Task Publish(T message);
}