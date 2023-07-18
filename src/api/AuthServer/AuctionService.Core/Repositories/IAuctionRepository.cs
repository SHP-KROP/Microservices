using AuctionService.Core.Entities;

namespace AuctionService.Core.Repositories;

public interface IAuctionRepository
{ 
    Task<bool> CreateAuction(Auction auction);
}