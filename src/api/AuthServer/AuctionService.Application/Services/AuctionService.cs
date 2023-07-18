using AuctionService.Application.Models.Auction;
using AuctionService.Application.Services.Abstractions;
using AuctionService.Core.Entities;
using AuctionService.Core.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace AuctionService.Application.Services;

public sealed class AuctionService : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly ILogger<AuctionService> _logger;

    public AuctionService(IAuctionRepository auctionRepository, ILogger<AuctionService> logger)
    {
        _auctionRepository = auctionRepository;
        _logger = logger;
    }
    
    public async Task<Result<AuctionViewModel>> Create(AuctionCreateModel createModel, string userId)
    {
        _logger.LogInformation("Started creating auction with Id {@AuctionId} for user {@UserId}", 
            createModel.Id, userId);

        Auction auction = createModel;
        auction.UserId = Guid.Parse(userId);
        
        var result = await _auctionRepository.CreateAuction(auction);

        if (result)
        {
            _logger.LogInformation("Auction with Id {@AuctionId} created", auction.Id);

            return Result.Ok((AuctionViewModel)auction);
        }

        _logger.LogWarning("Failed to create auction with Id {@AuctionId}", auction.Id);
        
        return Result.Fail($"Unable to create an auction with Id {auction.Id}");
    }
}