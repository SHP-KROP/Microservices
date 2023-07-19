using AuctionService.Application.Models.Auction;
using AuctionService.Core.Entities;
using AuctionService.Core.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using AuctionService = AuctionService.Application.Services.AuctionService;

namespace AuctionService.Application.UnitTests;

public class AuctionServiceTests
{
    private readonly IAuctionRepository _auctionRepository;
    
    private readonly Services.AuctionService _sut;

    public AuctionServiceTests()
    {
        _auctionRepository = Substitute.For<IAuctionRepository>();
        _sut = new(_auctionRepository, Substitute.For<ILogger<Services.AuctionService>>());
    }

    [Fact]
    public async void ItShouldPassCreateModelToRepository_WhenCreatingAuction()
    {
        var createModel = new AuctionCreateModel();
        
        await _sut.Create(createModel, Guid.NewGuid().ToString());

        await _auctionRepository.Received(1).CreateAuction(Arg.Is<Auction>(x => x.Id == createModel.Id));
    }
}