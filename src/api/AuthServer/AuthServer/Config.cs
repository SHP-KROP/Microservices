using Duende.IdentityServer.Models;

namespace AuthServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            // auction service
            new ("auction-read"),
            new ("auction-write"),
            new ("auction-manage")
        };

    public static IEnumerable<Client> Clients =>
        new []
        {
            // m2m client credentials flow for auction service
            new()
            {
                ClientId = "auction-service",
                ClientName = "Auction Service",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("auction-service-secret".Sha256()) },

                AllowedScopes = { "auction-read", "auction-write", "auction-manage" },
                AccessTokenType = AccessTokenType.Jwt
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" },
                RequireConsent = true
            },
        };
}