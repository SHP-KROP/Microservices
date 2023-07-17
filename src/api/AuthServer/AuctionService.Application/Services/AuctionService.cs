using AuctionService.Application.Models.Auction;
using AuctionService.Application.Services.Abstractions;
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
    
    public async Task<Result<AuctionViewModel>> Create(AuctionCreateModel createModel)
    {
        _logger.LogInformation("Started creating auction with Id {@AuctionId}", createModel.Id);
        
        var result = await _auctionRepository.CreateAuction(createModel);

        if (result)
        {
            _logger.LogInformation("Auction with Id {@AuctionId} created", createModel.Id);

            return Result.Ok((AuctionViewModel)createModel);
        }

        _logger.LogWarning("Failed to create auction with Id {@AuctionId}", createModel.Id);
        
        return Result.Fail($"Unable to create an auction with Id {createModel.Id}");
    }
}