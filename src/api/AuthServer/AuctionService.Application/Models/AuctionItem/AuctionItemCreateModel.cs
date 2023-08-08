using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace AuctionService.Application.Models.AuctionItem;

public class AuctionItemCreateModel
{
    public decimal StartingPrice { get; set; }

    public decimal MinimalBid { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public IEnumerable<IFormFile> Photos { get; set; }

    public static implicit operator Core.Entities.AuctionItem(AuctionItemCreateModel model) =>
        new()
        {
            StartingPrice = model.StartingPrice,
            MinimalBid = model.MinimalBid,
            Name = model.Name,
            Description = model.Description
        };

    public static explicit operator AuctionItemCreateModel(Core.Entities.AuctionItem auctionItem)
    {
        return new AuctionItemCreateModel
        {
            StartingPrice = auctionItem.StartingPrice,
            MinimalBid = auctionItem.MinimalBid,
            Name = auctionItem.Name,
            Description = auctionItem.Description
        };
    }
}