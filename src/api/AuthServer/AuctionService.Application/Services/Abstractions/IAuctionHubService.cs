using AuctionService.Application.Events;
using AuctionService.Application.Models.AuctionItem;
using FluentResults;

namespace AuctionService.Application.Services.Abstractions
{
    public interface IAuctionHubService
    {
        Task<Result<AuctionItemCreateModel>> CreateBid(BidUpdatedEvent bidUpdatedEvent, string userId);
    }
}
