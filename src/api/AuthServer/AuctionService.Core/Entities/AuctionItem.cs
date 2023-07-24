namespace AuctionService.Core.Entities;

public class AuctionItem
{
    public Guid Id { get; set; }
    
    public decimal StartingPrice { get; set; }

    public decimal ActualPrice { get; set; }

    public decimal MinimalBid { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsSellingNow { get; set; }

    public bool IsSold { get; set; }

    public ICollection<AuctionItemPhoto> Photos { get; set; }
}