using AuctionService.Application.Services.Abstractions;
using AuctionService.Core.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AuctionService.Application.Services.Singleton;

public class InMemoryAuctionStorage : IAuctionStorage
{
    private const string AuctionsCacheKey = "Auctions";
    
    private readonly ILogger<InMemoryAuctionStorage> _logger;
    private readonly IMemoryCache _cache;

    public InMemoryAuctionStorage(ILogger<InMemoryAuctionStorage> logger, IMemoryCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    public async Task<Dictionary<Guid, Auction>> GetAuctions()
    {
        _logger.LogInformation("Getting auctions from in-memory store");
        return _cache.Get<Dictionary<Guid, Auction>>(AuctionsCacheKey) ?? new Dictionary<Guid, Auction>();
    }

    public async Task AddAuctions(IEnumerable<Auction> auctions)
    {
        var storedAuctions = await GetAuctions();
        var auctionsDictionary = auctions.ToDictionary(x => x.Id, x => x);

        var resultAuctions = auctionsDictionary
            .Concat(storedAuctions)
            .ToDictionary(x => x.Key, x => x.Value);
        
        _logger.LogInformation("Adding auctions with ids {@Auctionids} to in-memory store", resultAuctions.Keys);
        _cache.Set(AuctionsCacheKey, resultAuctions);
    }
}