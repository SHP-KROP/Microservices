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
            return await _context.SaveChangesAsync() > 0;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
}