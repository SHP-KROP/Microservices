using AuthServer.DTO;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Exceptions
{
    public class CreatingUserException : InvalidOperationException
    {
        public CreatingUserException(IEnumerable<IdentityError> errors)
        : base($"Failed to create user: {string.Join(", ", errors.Select(error => error.Description))}")
        {
        }
    }
}
