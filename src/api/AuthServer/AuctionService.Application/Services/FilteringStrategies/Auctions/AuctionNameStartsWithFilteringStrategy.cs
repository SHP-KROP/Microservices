using System.Linq.Expressions;
using AuctionService.Application.Services.Abstractions;
using AuctionService.Core.Entities;

namespace AuctionService.Application.Services.FilteringStrategies.Auctions;

internal sealed class AuctionNameStartsWithFilteringStrategy : FilteringStrategyBase<Auction>
{
    private AuctionNameStartsWithFilteringStrategy(Expression<Func<Auction, bool>> predicate) 
        : base(predicate){}

    public static AuctionNameStartsWithFilteringStrategy Create(string? nameStartsWith)
    {
        if (string.IsNullOrWhiteSpace(nameStartsWith))
        {
            return new(null);
        }
        
        return new(x => x.Name.StartsWith(nameStartsWith));
    }
}