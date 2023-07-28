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