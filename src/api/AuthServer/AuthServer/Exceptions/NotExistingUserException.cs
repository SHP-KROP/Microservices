using AuthServer.DTO;

namespace AuthServer.Exceptions
{
    public class NotExistingUserException : InvalidOperationException
    {
        public NotExistingUserException(string email)
        : base($"There is no user with email {email}")
        {
        }
    }
}
