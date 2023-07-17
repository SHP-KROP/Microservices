using AuthServer.DTO;

namespace AuthServer.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException()
        : base($"Wrong password")
        {
        }
    }
}
