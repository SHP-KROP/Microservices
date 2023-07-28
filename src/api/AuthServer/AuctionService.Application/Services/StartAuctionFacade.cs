using AuctionService.Application.Services.Abstractions.Facades;
using AuctionService.Core.Entities;
using AuctionService.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace AuctionService.Application.Services;

public class StartAuctionFacade : IStartAuctionFacade
{
    private readonly ILogger<StartAuctionFacade> _logger;
    private readonly IAuctionRepository _auctionRepository;

    public StartAuctionFacade(ILogger<StartAuctionFacade> logger, IAuctionRepository auctionRepository)
    {
        _logger = logger;
        _auctionRepository = auctionRepository;
    }
    
    public async Task<IEnumerable<Auction>> StartAuctions(IEnumerable<Guid> auctionIds)
    {
        _logger.LogInformation("Starting auctions with ids {@AuctionIds}", auctionIds);
        
        var auctions = await _auctionRepository.GetAuctionsByIds(auctionIds);
        
        foreach (var auction in auctions)
        {
            var firstItem = auction.GetFirstItem();

            if (firstItem is null)
            {
                auction.EndTime = DateTimeOffset.Now;
                continue;
            }

            firstItem.IsSellingNow = true;
        }
        
        await _auctionRepository.Commit();
        _logger.LogInformation("Auctions with ids {@AuctionIds} were started successfully", auctionIds);

        return auctions;
    }
}