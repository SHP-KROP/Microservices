namespace ServiceRegistration.Options;

internal sealed class ConsulOptions
{
    public const string Section = "Consul";
    
    public string Url { get; init; }
}