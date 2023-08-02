using AuctionService.Application.Models.Auction;
using AuctionService.Core.Entities;

namespace AuctionService.Application.Services.Abstractions.Repositories;

public interface IAuctionRepository
{ 
    Task<bool> CreateAuction(Auction auction);

    Task<Auction> GetAuctionById(Guid id);

    Task<IEnumerable<Auction>> GetAuctionsByIds(IEnumerable<Guid> ids);

    Task<IEnumerable<Guid>> GetReadyToStartAuctionsSince(DateTimeOffset timeFrom);

    Task<CursorPaginatedAuctions> GetFilteredPagedAuctions(AuctionCursorPagingFilteringModel filteredPagingModel);
    
    Task<bool> Commit();
    Task<bool> ExistsWithName(string name);
}