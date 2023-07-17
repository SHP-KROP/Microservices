using AuctionService.Core.Enums;

namespace AuctionService.Core.Entities;

public class Auction
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
    
    public DateTimeOffset? StartTime { get; set; }

    public DateTimeOffset? EndTime { get; set; }

    public AuctionType AuctionType { get; set; }
}