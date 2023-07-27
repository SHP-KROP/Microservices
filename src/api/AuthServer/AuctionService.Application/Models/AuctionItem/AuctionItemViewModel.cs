namespace AuctionService.Application.Models.AuctionItem;

public class AuctionItemViewModel
{
    public Guid Id { get; set; }
    
    public decimal StartingPrice { get; set; }

    public decimal ActualPrice { get; set; }

    public decimal MinimalBid { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsSellingNow { get; set; }

    public bool IsSold { get; set; }

    public IEnumerable<PhotoViewModel> Photos { get; set; }

    public static implicit operator AuctionItemViewModel(Core.Entities.AuctionItem auctionItem) =>
        new()
        {
            Id = auctionItem.Id,
            Description = auctionItem.Description,
            Name = auctionItem.Name,
            ActualPrice = auctionItem.ActualPrice,
            MinimalBid = auctionItem.MinimalBid,
            IsSold = auctionItem.IsSold,
            StartingPrice = auctionItem.StartingPrice,
            IsSellingNow = auctionItem.IsSellingNow,
            Photos = auctionItem.Photos.Select(x => new PhotoViewModel
            {
                Name = x.Name,
                Url = x.PhotoUrl
            })
        };
}