using AuctionService.Application.Events;
using AuctionService.Application.Models.AuctionItem;
using AuctionService.Application.Services.Abstractions;
using AuctionService.Application.Services.Abstractions.Repositories;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace AuctionService.Application.Services
{
    public class AuctionHubService : IAuctionHubService
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly ILogger<AuctionService> _logger;

        public AuctionHubService(IAuctionRepository auctionRepository, ILogger<AuctionService> logger)
        {
            _auctionRepository = auctionRepository;
            _logger = logger;
        }

        public async Task<Result<AuctionItemCreateModel>> CreateBid(BidUpdatedEvent updatedEvent, string userId)
        {
            var auctionItem = await _auctionRepository.GetAuctionItemById(Guid.Parse(updatedEvent.AuctionItemId));

            if (auctionItem is null)
            {
                return Result.Fail($"There is no auction item with id {updatedEvent.AuctionItemId}");
            }

            var date = DateTimeOffset.Now;

            var result = auctionItem.AddBid(Guid.Parse(userId), updatedEvent.UpdatedPrice, date);

            if (!result.Equals(null))
            {
                _logger.LogInformation("Bid in auction item with Id {@AuctionItemId} created", auctionItem.Id);

                return Result.Ok((AuctionItemCreateModel)auctionItem);
            }

            _logger.LogWarning("Failed to create a bid in auction item with Id {@AuctionId}", auctionItem.Id);

            return Result.Fail($"Unable to create a bid in auction item with Id {auctionItem.Id}");
        }
    }
}
