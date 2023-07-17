using AuthServer.DTO;
using FluentValidation;

namespace AuthServer.Valdators
{
    public class UserLoginValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(user => user.Password)
                .SetValidator(new PasswordValidator());
        }
    }
}
