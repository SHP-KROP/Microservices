using FluentValidation;

namespace AuctionService.Application.Models.Auction.Validators;

public sealed class AuctionCreateModelValidator : AbstractValidator<AuctionCreateModel>
{
    public AuctionCreateModelValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).MinimumLength(3);
        RuleFor(x => x.Description).MaximumLength(200);
    }
}