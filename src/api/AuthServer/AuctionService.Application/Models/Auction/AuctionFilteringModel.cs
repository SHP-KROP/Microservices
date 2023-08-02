namespace AuctionService.Application.Models.Auction;

public sealed class AuctionFilteringModel
{
    public string? NameStartsWith { get; init; }

    public string? DescriptionContains { get; init; }
}