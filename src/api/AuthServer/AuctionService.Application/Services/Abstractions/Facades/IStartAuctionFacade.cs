using AuctionService.Core.Entities;

namespace AuctionService.Application.Services.Abstractions.Facades;

public interface IStartAuctionFacade
{
    Task<IEnumerable<Auction>> StartAuctions(IEnumerable<Guid> auctionIds);
}