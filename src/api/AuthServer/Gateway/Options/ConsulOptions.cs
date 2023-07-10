namespace Discovery.Options;

public sealed record ConsulOptions(string Url)
{
    public const string Section = "Consul";
}