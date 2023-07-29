using System.Collections;
using AuctionService.Core.Entities;
using AuctionService.Core.Enums;
using AuctionService.Infrastructure.Persistence;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Extensions;

public static class WebApplicationExtensions
{
    internal static async Task SeedData(this WebApplication @this)
    {
        await using var scope = @this.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();

        await context.WipeAllData();
        await context.FillDomainData();
    }

    private static async Task FillDomainData(this AuctionDbContext @this)
    {
        var auctionItemPhotoFaker = CreateAuctionItemPhotoFaker();
        var auctionItemFaker = CreateAuctionItemFaker(auctionItemPhotoFaker);
        var auctionFaker = CreateAuctionFaker(auctionItemFaker);

        var auctionsToSeed = auctionFaker.Generate(50);

        FillBids(auctionsToSeed);

        await @this.Auctions.AddRangeAsync(auctionsToSeed);
        await @this.SaveChangesAsync();
    }

    private static void FillBids(IEnumerable<Auction> auctions)
    {
        foreach (var auction in auctions)
        {
            foreach (var item in auction.AuctionItems)
            {
                var random = new Random((int)DateTime.Now.Ticks);

                item.AddBid(
                    Guid.NewGuid(), 
                    item.MinimalBid + (random.Next() % item.MinimalBid),
                    DateTimeOffset.Now.AddMinutes(random.Next() % 3600));
            }
        }
    }

    private static Faker<AuctionItemPhoto> CreateAuctionItemPhotoFaker()
    {
        var auctionItemPhotoUrl = new Faker<AuctionItemPhoto>()
            .RuleFor(p => p.Id, _ => 0)
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.PhotoUrl, f => f.Image.PicsumUrl());
        return auctionItemPhotoUrl;
    }

    private static Faker<AuctionItem> CreateAuctionItemFaker(Faker<AuctionItemPhoto> auctionItemPhotoUrl)
    {
        var auctionItemFaker = new Faker<AuctionItem>()
            .RuleFor(a => a.Id, f => f.Random.Guid())
            .RuleFor(a => a.StartingPrice, f => f.Random.Decimal(10, 100))
            .RuleFor(a => a.ActualPrice, f => f.Random.Decimal(5, 50))
            .RuleFor(a => a.MinimalBid, f => f.Random.Decimal(1, 10))
            .RuleFor(a => a.Name, f => f.Commerce.ProductName())
            .RuleFor(a => a.Description, f => f.Lorem.Sentence())
            .RuleFor(a => a.IsSellingNow, f => f.Random.Bool())
            .RuleFor(a => a.IsSold, f => f.Random.Bool())
            .RuleFor(a => a.Photos, f => auctionItemPhotoUrl.Generate(f.Random.Number(1, 5)));

        return auctionItemFaker;
    }

    private static Faker<Auction> CreateAuctionFaker(Faker<AuctionItem> auctionItemFaker)
    {
        var auctionFaker = new Faker<Auction>()
            .RuleFor(a => a.Id, f => f.Random.Guid())
            .RuleFor(a => a.UserId, f => f.Random.Guid())
            .RuleFor(a => a.Name, f => f.Commerce.ProductName() + Guid.NewGuid().ToString()[..6])
            .RuleFor(a => a.Description, f => f.Lorem.Sentence())
            .RuleFor(a => a.StartTime, f => f.Date.FutureOffset())
            .RuleFor(a => a.EndTime, _ => null)
            .RuleFor(a => a.AuctionType, _ => AuctionType.English)
            .RuleFor(a => a.AuctionItems, f => auctionItemFaker.Generate(f.Random.Number(3, 7)));

        return auctionFaker;
    }

    private static async Task WipeAllData(this AuctionDbContext @this)
    {
        await @this.RemoveData<Auction>();
        await @this.RemoveData<AuctionItem>();
        await @this.RemoveData<AuctionItemPhoto>();
        await @this.RemoveData<Bid>();
        
        await @this.SaveChangesAsync();
    }
    
    private static async Task RemoveData<T>(this AuctionDbContext @this)
        where T : class 
        => await @this.Set<T>().ExecuteDeleteAsync();
}