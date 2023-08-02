using AuctionService.Core.Entities;

namespace AuctionService.Application.Services.Abstractions;

public interface IAuctionStorage
{
    Task<Dictionary<Guid, Auction>> GetAuctions();
    
    Task AddAuctions(IEnumerable<Auction> aucitons);
}