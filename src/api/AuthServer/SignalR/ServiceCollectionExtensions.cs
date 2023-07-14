using Microsoft.Extensions.DependencyInjection;

namespace SignalR;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRealTimeMessaging(this IServiceCollection @this)
    {
        @this.AddSignalR();

        return @this;
    }
}