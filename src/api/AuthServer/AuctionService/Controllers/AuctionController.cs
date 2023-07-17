using AuctionService.Application.Models.Auction;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionController : Controller
{
    public AuctionController()
    {
        
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuctionCreateModel createModel)
    {
        return Ok();
    }
}