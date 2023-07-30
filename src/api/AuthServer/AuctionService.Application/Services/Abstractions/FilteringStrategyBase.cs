using System.Linq.Expressions;

namespace AuctionService.Application.Services.Abstractions;

public abstract class FilteringStrategyBase<TEntity> : IFilteringStrategy<TEntity>
{
    protected FilteringStrategyBase(Expression<Func<TEntity, bool>> predicate)
    {
        Predicate = predicate;
    }

    private Expression<Func<TEntity, bool>> Predicate { get; }
    
    public IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> entities)
        => Predicate is null ? entities : entities.Where(Predicate);
}