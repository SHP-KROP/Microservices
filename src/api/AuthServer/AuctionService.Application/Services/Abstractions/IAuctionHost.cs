using AuctionService.Core.Entities;

namespace AuctionService.Application.Services.Abstractions;

public interface IAuctionHost
{
    Task<IReadOnlyDictionary<Guid, Auction>> GetActiveAuctions();
    
    Task StartAuctions(IEnumerable<Guid> auctionIds);
}