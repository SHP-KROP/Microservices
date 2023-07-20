using System.Security.Claims;
using AuctionService.Application.Models.Auction;
using AuctionService.Application.Models.AuctionItem;
using AuctionService.Application.Services.Abstractions;
using AuctionService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuctionsController : Controller
{
    private readonly IAuctionService _auctionService;

    public AuctionsController(IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuctionCreateModel createModel)
    {
        var result = await _auctionService.Create(createModel, User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        return result.ToResponse();
    }
    
    [HttpPost("{auctionId:guid}/items")]
    public async Task<IActionResult> AddItem(Guid auctionId, [FromForm] AuctionItemCreateModel createModel)
    {
        var result = await _auctionService.AddItem(
            auctionId, createModel, User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        return result.ToResponse();
    }
}