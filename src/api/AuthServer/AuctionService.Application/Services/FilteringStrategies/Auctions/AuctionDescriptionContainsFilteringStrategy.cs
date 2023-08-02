using System.Linq.Expressions;
using AuctionService.Application.Services.Abstractions;
using AuctionService.Core.Entities;

namespace AuctionService.Application.Services.FilteringStrategies.Auctions;

internal sealed class AuctionDescriptionContainsFilteringStrategy : FilteringStrategyBase<Auction>
{
    private AuctionDescriptionContainsFilteringStrategy(Expression<Func<Auction, bool>> predicate) 
        : base(predicate){}

    public static AuctionDescriptionContainsFilteringStrategy Create(string? descriptionContains)
    {
        if (string.IsNullOrWhiteSpace(descriptionContains))
        {
            return new(null);
        }

        return new(x => x.Description.Contains(descriptionContains));
    }
}