namespace ServiceRegistration.Options;

internal sealed class DiscoveryOptions
{
    public const string Section = "Discovery";

    public string ServiceName { get; init; }
    
    public string ServiceId { get; init; }
    
    public string Url { get; init; }
}