using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToResponse<T>(this Result<T> @this)
        => @this.IsSuccess
            ? new OkObjectResult(@this.Value)
            : new BadRequestObjectResult(string.Join(Environment.NewLine, @this.Errors));
}