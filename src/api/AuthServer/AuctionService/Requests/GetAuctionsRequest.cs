using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Requests;

public sealed class GetAuctionsRequest
{
    [FromQuery(Name = "pageSize")]
    public int PageSize { get; init; }

    [FromQuery(Name = "cursor")]
    public string? Cursor { get; init; }

    [FromQuery(Name = "name.[sw]")] 
    public string? NameStartsWith { get; set; }

    [FromQuery(Name = "description.[contains]")]
    public string? DescriptionContains { get; set; }

    public static implicit operator Application.Models.Auction.AuctionFilteringModel(GetAuctionsRequest model)
        => new()
        {
            NameStartsWith = model?.NameStartsWith,
            DescriptionContains = model?.DescriptionContains
        };
}