namespace Authentication.Options;

internal sealed class AuthenticationOptions
{
    public const string Section = "Authentication";

    public string Issuer { get; init; }
    
    public string SecurityKey { get; init; }
}