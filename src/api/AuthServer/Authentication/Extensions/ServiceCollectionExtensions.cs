using System.Text;
using Authentication.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServerAuthentication(this IServiceCollection @this, IConfiguration configuration)
    {
        var authOptions = new AuthenticationOptions();
        configuration.GetSection(AuthenticationOptions.Section).Bind(authOptions);
        
        @this.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,
                    
                    ValidateAudience = false,

                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SecurityKey)),
                    ValidateIssuerSigningKey = true,
                };
            });
        
        return @this;
    }
}