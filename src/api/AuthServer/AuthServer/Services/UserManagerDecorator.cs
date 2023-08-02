using AuthServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Services;

public sealed class UserManagerDecorator : IUserManagerDecorator
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserManagerDecorator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public Task<ApplicationUser> FindByEmailAsync(string email)
        => _userManager.FindByEmailAsync(email);

    public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        => _userManager.CreateAsync(user, password);

    public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        => _userManager.CheckPasswordAsync(user, password);
}