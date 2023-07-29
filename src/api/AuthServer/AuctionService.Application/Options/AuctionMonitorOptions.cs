namespace AuctionService.Application.Options;

public sealed class AuctionMonitorOptions
{
    public const string Section = "AuctionMonitor";

    public int MonitoringIntervalSeconds { get; set; }
}