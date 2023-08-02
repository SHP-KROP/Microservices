using AuctionService.Application.Helpers;
using AuctionService.Application.Models.Auction;
using AuctionService.Application.Services.Abstractions.Repositories;
using AuctionService.Application.Services.FilteringStrategies.Auctions;
using AuctionService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuctionService.Infrastructure.Persistence.Repositories;

public sealed class AuctionRepository : IAuctionRepository
{
    private readonly AuctionDbContext _context;
    private readonly ILogger<AuctionRepository> _logger;

    public AuctionRepository(AuctionDbContext context, ILogger<AuctionRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CursorPaginatedAuctions> GetFilteredPagedAuctions(AuctionCursorPagingFilteringModel filteredPagingModel)
    {
        var auctions = _context.Auctions.OrderByDescending(x => x.StartTime).AsQueryable();

        if (filteredPagingModel.Filter is not null)
        {
            var filteringStrategy = new AuctionFilteringStrategy(filteredPagingModel.Filter);
            auctions = filteringStrategy.ApplyFilter(auctions);
        }

        if (filteredPagingModel.Cursor is not null)
        {
            auctions = auctions.Where(x => x.StartTime < filteredPagingModel.Cursor);
        }

        var auctionsResult = await auctions.Take(filteredPagingModel.PageSize + 1).ToListAsync();

        string nextCursor = null;
        
        if (auctionsResult.Count == filteredPagingModel.PageSize + 1)
        {
            nextCursor = CursorConverter.EncodeAuctionCursor(auctionsResult[^2].StartTime);
        }

        var result = new CursorPaginatedAuctions
        {
            Auctions = auctionsResult.Take(filteredPagingModel.PageSize),
            Cursor = nextCursor,
        };

        return result;
    }
    
    public async Task<IEnumerable<Auction>> GetAuctionsByIds(IEnumerable<Guid> ids)
    {
        var auctions = _context.Auctions
            .Include(x => x.AuctionItems)
            .ThenInclude(x => x.Bids)
            .Where(auction => ids.Contains(auction.Id));

        return await auctions.ToListAsync();
    }

    public async Task<IEnumerable<Guid>> GetReadyToStartAuctionsSince(DateTimeOffset timeFrom)
    {
        var auctionsReadyToBeStarted = _context.Auctions
            .Include(x => x.AuctionItems)
            .Where(auction => 
                auction.EndTime == null
                && auction.StartTime < timeFrom
                && !auction.AuctionItems.Any(item => item.IsSellingNow));

        return await auctionsReadyToBeStarted.Select(x => x.Id).ToListAsync();
    }
    
    public async Task<bool> CreateAuction(Auction auction)
    {
        await _context.Auctions.AddAsync(auction);

        try
        {
            return await Commit();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
    
    public async Task<bool> ExistsWithName(string name)
    {
        return await _context.Auctions.AnyAsync(x => x.Name == name);
    }

    public async Task<Auction> GetAuctionById(Guid id)
    {
        return await _context.Auctions
            .Include(x => x.AuctionItems)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> Commit()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}