namespace AuctionService.Application.Services.Abstractions;

public interface IFilteringStrategy<TEntity>
{
    public IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> entities);
}