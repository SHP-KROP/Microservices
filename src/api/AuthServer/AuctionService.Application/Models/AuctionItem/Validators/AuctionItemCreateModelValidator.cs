using FluentValidation;

namespace AuctionService.Application.Models.AuctionItem.Validators;

public sealed class AuctionItemCreateModelValidator : AbstractValidator<AuctionItemCreateModel>
{
    public AuctionItemCreateModelValidator()
    {
        RuleFor(x => x.StartingPrice).GreaterThanOrEqualTo(0);
        RuleFor(x => x.MinimalBid).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Photos).NotEmpty();
    }
}