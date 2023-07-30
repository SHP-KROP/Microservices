namespace AuctionService.Application.Models.Auction;

public sealed class AuctionViewModel
{
    public Guid Id { get; init; }
    
    public Guid UserId { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public DateTimeOffset StartTime { get; init; }

    public DateTimeOffset? EndTime { get; init; }

    public string AuctionType { get; init; }
    
    // TODO: To be implemented in future stories
    public string PhotoUrl { get; init; } = "https://some-photo-url-placeholder";

    public static implicit operator AuctionViewModel(Core.Entities.Auction auction) =>
        new()
        {
            Id = auction.Id,
            UserId = auction.UserId,
            AuctionType = auction.AuctionType.ToString(),
            Name = auction.Name,
            Description = auction.Description,
            EndTime = auction.EndTime,
            StartTime = auction.StartTime,
        };
}