using AuctionService.Application.Models.Auction;
using AuctionService.Application.Services.Abstractions;
using FluentResults;

namespace AuctionService.Application.Services;

public sealed class AucitonService : IAuctionService
{
    public AucitonService()
    {
        
    }
    
    public async Task<Result<AuctionViewModel>> Create(AuctionCreateModel createModel)
    {
        return Result.Fail("asd");
    }
}