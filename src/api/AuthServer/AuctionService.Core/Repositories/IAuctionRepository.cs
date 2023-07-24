using AuctionService.Core.Entities;

namespace AuctionService.Core.Repositories;

public interface IAuctionRepository
{ 
    Task<bool> CreateAuction(Auction auction);

    Task<Auction> GetAuctionById(Guid id);

    Task<IEnumerable<Auction>> GetAuctionsByIds(IEnumerable<Guid> ids);

    Task<IEnumerable<Guid>> GetReadyToStartAuctionsSince(DateTimeOffset timeFrom);

    Task<bool> Commit();
}