using AuthServer.DTO;

namespace AuthServer.Exceptions
{
    public class WrongPasswordException : InvalidOperationException
    {
        public WrongPasswordException()
        : base($"Wrong password")
        {
        }
    }
}
