using Microsoft.AspNetCore.SignalR;

namespace AuctionService.Hubs;

public sealed class AuctionHub : Hub
{
    private readonly ILogger<AuctionHub> _logger;

    public AuctionHub(ILogger<AuctionHub> logger)
    {
        _logger = logger;
    }
    
    public Task UpdateBid(Guid productId, decimal newPrice)
    {
        Clients.All.SendAsync("BidUpdated", productId, newPrice);

        return Task.CompletedTask;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("Client {@ClientId} connected to Auction hub", Context.ConnectionId);
        
        return base.OnConnectedAsync();
    }
}