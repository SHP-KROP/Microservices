using AuctionService.Core.Entities;
using FluentAssertions;

namespace AuctionService.Core.UnitTests;

public class AuctionItemTests
{
    [Fact]
    public void AddBid_ShouldUpdateActualPrice()
    {
        const decimal actualPrice = 100, priceToAdd = 10;
        var item = new AuctionItem { ActualPrice = actualPrice };

        item.AddBid(default, priceToAdd, default);

        item.ActualPrice.Should().Be(actualPrice + priceToAdd);
    }
    
    [Fact]
    public void AddBid_ShouldAddBidToCollection()
    {
        var item = new AuctionItem();

        var expectedBid = item.AddBid(default, default, default);

        item.Bids.Should().Contain(expectedBid);
    }
}