using AuctionService.Application.Models.Auction;
using AuctionService.Application.Services.Abstractions;
using AuctionService.Application.Services.Abstractions.Repositories;
using AuctionService.Core.Entities;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace AuctionService.Application.UnitTests;

public class AuctionServiceTests
{
    private readonly IAuctionRepository _auctionRepository;
    
    private readonly Services.AuctionService _sut;

    public AuctionServiceTests()
    {
        _auctionRepository = Substitute.For<IAuctionRepository>();
        _sut = new(
            _auctionRepository, 
            Substitute.For<ILogger<Services.AuctionService>>(), 
            Substitute.For<IBlobService>());
    }

    [Fact]
    public async void ItShouldPassCreateModelToRepository_WhenCreatingAuction()
    {
        var createModel = new AuctionCreateModel();
        
        await _sut.Create(createModel, Guid.NewGuid().ToString());

        await _auctionRepository.Received(1).CreateAuction(Arg.Is<Auction>(x => x.Id == createModel.Id));
    }
}