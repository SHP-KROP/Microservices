using AuctionService.Application.Models.Auction;
using AuctionService.Application.Services.Abstractions;
using AuctionService.Core.Entities;

namespace AuctionService.Application.Services.FilteringStrategies.Auctions;

public sealed class AuctionFilteringStrategy : IFilteringStrategy<Auction>
{
    private readonly ICollection<IFilteringStrategy<Auction>> _strategies = new List<IFilteringStrategy<Auction>>();
    
    public AuctionFilteringStrategy(AuctionFilteringModel filter)
    {
        _strategies.Add(AuctionDescriptionContainsFilteringStrategy.Create(filter.DescriptionContains));
        _strategies.Add(AuctionNameStartsWithFilteringStrategy.Create(filter.NameStartsWith));
    }

    public IQueryable<Auction> ApplyFilter(IQueryable<Auction> entities)
    {
        foreach (var strategy in _strategies)
        {
            entities = strategy.ApplyFilter(entities);
        }

        return entities;
    }
}