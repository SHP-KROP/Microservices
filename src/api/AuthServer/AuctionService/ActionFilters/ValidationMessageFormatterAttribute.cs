using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuctionService.ActionFilters;

public sealed class ValidationMessageFormatterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.ModelState.IsValid) return;
        
        var errors = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage);

        var errorMessage = string.Join(Environment.NewLine, errors);

        context.Result = new BadRequestObjectResult(errorMessage);
    }
}