namespace AuctionService.Application.Models.Auction;

public sealed class CursorPaginatedAuctionsViewModel
{
    public string? Cursor { get; set; }

    public IEnumerable<AuctionViewModel> Auctions { get; set; }

    public static implicit operator CursorPaginatedAuctionsViewModel(CursorPaginatedAuctions paginatedAuctions)
        => new()
        {
            Cursor = paginatedAuctions.Cursor,
            Auctions = paginatedAuctions.Auctions.Select(x => (AuctionViewModel)x)
        };
}