namespace AuthServer.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(string userId, string userName, string email);
    }
}
