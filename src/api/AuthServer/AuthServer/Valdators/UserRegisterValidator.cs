using AuthServer.DTO;
using FluentValidation;

namespace AuthServer.Valdators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDTO>
    {
        public UserRegisterValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(user => user.Surname)
                .NotEmpty().WithMessage("Surname is required.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(user => user.Password)
                .SetValidator(new PasswordValidator());
        }
    }
}
