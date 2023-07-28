using AuctionService.Application.Services.Abstractions;
using AuctionService.Application.Services.Abstractions.Facades;
using AuctionService.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionService.Application.Services.Singleton;

public sealed class AuctionHost : IAuctionHost
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IAuctionStorage _auctionStorage;

    public AuctionHost(IServiceScopeFactory serviceScopeFactory, IAuctionStorage auctionStorage)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _auctionStorage = auctionStorage;
    }

    public async Task<IReadOnlyDictionary<Guid, Auction>> GetActiveAuctions() => await _auctionStorage.GetAuctions();

    public async Task StartAuctions(IEnumerable<Guid> auctionIds)
    {
        using var scope = _serviceScopeFactory.CreateScope(); 
        var startAuctionFacade = scope.ServiceProvider.GetRequiredService<IStartAuctionFacade>();
        
        var auctions = await startAuctionFacade.StartAuctions(auctionIds);

        await _auctionStorage.AddAuctions(auctions);
    }
}