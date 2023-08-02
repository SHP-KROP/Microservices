using Microsoft.AspNetCore.Identity;

namespace AuthServer.Services.Interfaces;

public interface IUserManagerDecorator
{
    Task<ApplicationUser?> FindByEmailAsync(string email);

    Task<IdentityResult> CreateAsync(ApplicationUser user, string password);

    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
}