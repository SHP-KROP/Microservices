using AuctionService.Application.Models.Auction;
using AuctionService.Application.Models.AuctionItem;
using AuctionService.Application.Services.Abstractions;
using AuctionService.Application.Services.Abstractions.Repositories;
using AuctionService.Core.Entities;
using AuctionService.Application.Events;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace AuctionService.Application.Services;

public sealed class AuctionService : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly ILogger<AuctionService> _logger;
    private readonly IBlobService _blobService;

    public AuctionService(IAuctionRepository auctionRepository, ILogger<AuctionService> logger, IBlobService blobService)
    {
        _auctionRepository = auctionRepository;
        _logger = logger;
        _blobService = blobService;
    }

    public async Task<Result<CursorPaginatedAuctionsViewModel>> GetFilteredPagedAuctions(
        int pageSize, string cursor, AuctionFilteringModel filter)
    {
        try
        {
            var filteringModel = AuctionCursorPagingFilteringModel.Create(pageSize, cursor, filter);
            
            var result = await _auctionRepository.GetFilteredPagedAuctions(filteringModel);

            return Result.Ok((CursorPaginatedAuctionsViewModel)result);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return Result.Fail(ex.Message);
        }
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

    public async Task<Result<AuctionItemViewModel>> AddItem(Guid auctionId, AuctionItemCreateModel createModel, string userId)
    {
        var auction = await _auctionRepository.GetAuctionById(auctionId);

        if (auction is null)
        {
            return Result.Fail($"There is no auction with id {auctionId}");
        }

        if (auction.UserId != Guid.Parse(userId))
        {
            return Result.Fail($"User with id {userId} is not owner of this auction");
        }

        var fileTasks = createModel.Photos
            .Select(x => _blobService.UploadFile(x, $"{auctionId}/{Guid.NewGuid()}" + Path.GetExtension(x.FileName)));
        
        var photos = (await Task.WhenAll(fileTasks)).Select(x => new AuctionItemPhoto
        {
            Name = x.fileName,
            PhotoUrl = x.url.ToString()
        }).ToList();

        AuctionItem auctionItem = createModel;
        auctionItem.Photos = photos;

        auction.AuctionItems.Add(auctionItem);

        if (!await _auctionRepository.Commit())
        {
            return Result.Fail("Unable to save changes while adding auction item");
        }

        return Result.Ok((AuctionItemViewModel)auctionItem);
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