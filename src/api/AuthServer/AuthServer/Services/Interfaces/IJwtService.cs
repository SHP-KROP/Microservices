namespace AuthServer.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(ApplicationUser applicationUser);
    }
}
