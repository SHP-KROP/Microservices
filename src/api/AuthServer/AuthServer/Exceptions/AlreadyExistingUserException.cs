namespace AuthServer.Exceptions
{
    public class AlreadyExistingUserException : InvalidOperationException
    {
        public AlreadyExistingUserException(string email)
        : base($"User with email {email} already exists")
        { 
        }
    }
}
