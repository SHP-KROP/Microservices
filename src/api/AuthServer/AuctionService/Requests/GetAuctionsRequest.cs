using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Requests;

public sealed class GetAuctionsRequest
{
    [FromQuery(Name = "pageSize")]
    public int PageSize { get; init; }

    [FromQuery(Name = "cursor")]
    public string? Cursor { get; init; }
}