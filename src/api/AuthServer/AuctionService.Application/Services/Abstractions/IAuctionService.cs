using AuctionService.Application.Models.Auction;
using FluentResults;

namespace AuctionService.Application.Services.Abstractions;

public interface IAuctionService
{
    public Task<Result<AuctionViewModel>> Create(AuctionCreateModel createModel);
}