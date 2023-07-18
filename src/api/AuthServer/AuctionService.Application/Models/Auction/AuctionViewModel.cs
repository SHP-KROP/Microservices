using AuctionService.Core.Enums;

namespace AuctionService.Application.Models.Auction;

public sealed class AuctionViewModel
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public DateTimeOffset? StartTime { get; init; }

    public DateTimeOffset? EndTime { get; init; }

    public AuctionType AuctionType { get; init; }

    public static implicit operator AuctionViewModel(Core.Entities.Auction auction) =>
        new()
        {
            Id = auction.Id,
            AuctionType = auction.AuctionType,
            Name = auction.Name,
            Description = auction.Description,
            EndTime = auction.EndTime,
            StartTime = auction.StartTime
        };
}