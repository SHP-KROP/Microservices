namespace AuctionService.Core.Entities;

public class Bid
{
    internal Bid(Guid userId, decimal amount, DateTimeOffset date, decimal actualPrice)
    {
        UserId = userId;
        Amount = amount;
        Date = date;
        ActualPrice = actualPrice;
    }

    public int Id { get; private set; }

    public Guid UserId { get; }

    public decimal Amount { get; }

    public DateTimeOffset Date { get; }

    public decimal ActualPrice { get; }
}