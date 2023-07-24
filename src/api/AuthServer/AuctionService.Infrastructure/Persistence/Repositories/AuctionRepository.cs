using System.Linq.Expressions;
using AuctionService.Core.Entities;
using AuctionService.Core.Repositories;
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