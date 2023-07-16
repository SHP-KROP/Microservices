namespace AuthServer.Exceptions
{
    public class AlreadyExistingUserException : Exception
    {
        public AlreadyExistingUserException(string message) : base(message)
        {
        }
    }
}
