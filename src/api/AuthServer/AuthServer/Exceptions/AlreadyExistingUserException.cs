namespace AuthServer.Exceptions
{
    public class AlreadyExistingUserException : Exception
    {
        public AlreadyExistingUserException(string email)
        : base($"User with email {email} already exists")
        { 
        }
    }
}
