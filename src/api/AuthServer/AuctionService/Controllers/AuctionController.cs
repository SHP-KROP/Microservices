using AuctionService.Application.Models.Auction;
using AuctionService.Application.Services.Abstractions;
using AuctionService.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionController : Controller
{
    private readonly IAuctionService _auctionService;

    public AuctionController(IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuctionCreateModel createModel)
    {
        var result = await _auctionService.Create(createModel);
        
        return result.ToResponse();
    }
}