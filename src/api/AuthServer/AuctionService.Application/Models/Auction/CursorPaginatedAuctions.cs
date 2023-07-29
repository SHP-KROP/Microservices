namespace AuctionService.Application.Models.Auction;

public class CursorPaginatedAuctions
{
    public string? Cursor { get; set; }

    public IEnumerable<Core.Entities.Auction> Auctions { get; set; }
}