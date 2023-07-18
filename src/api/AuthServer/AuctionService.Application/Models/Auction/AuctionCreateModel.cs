using AuctionService.Core.Enums;

namespace AuctionService.Application.Models.Auction;

public sealed class AuctionCreateModel
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; }

    public string Description { get; init; }

    public DateTimeOffset? StartTime { get; init; } = null;

    public AuctionType AuctionType { get; init; }

    public static implicit operator Core.Entities.Auction(AuctionCreateModel createModel) =>
        new()
        {
            Id = createModel.Id,
            AuctionType = createModel.AuctionType,
            Name = createModel.Name,
            Description = createModel.Description,
            StartTime = createModel.StartTime
        };
}