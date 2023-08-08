using AuctionService.Application.Events;
using AuctionService.Application.Models.Auction;
using AuctionService.Application.Models.AuctionItem;
using FluentResults;

namespace AuctionService.Application.Services.Abstractions;

public interface IAuctionService
{
    public Task<Result<AuctionViewModel>> Create(AuctionCreateModel createModel, string userId);
    
    Task<Result<AuctionItemViewModel>> AddItem(Guid auctionId, AuctionItemCreateModel createModel, string userId);

    Task<Result<CursorPaginatedAuctionsViewModel>> GetFilteredPagedAuctions(
        int pageSize, string cursor, AuctionFilteringModel filter);

    Task<Result<AuctionItemCreateModel>> CreateBid(BidUpdatedEvent bidUpdatedEvent, string userId);
}