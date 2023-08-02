using AuctionService.Application.Options;
using AuctionService.Application.Services.Abstractions;
using AuctionService.Application.Services.Singleton;
using AuctionService.HostedServices;
using AuctionService.Hubs;
using AuctionService.Infrastructure;
using AuctionService.Infrastructure.Messaging;
using AuctionService.Infrastructure.Messaging.Contracts;
using AuctionService.Infrastructure.Messaging.Options;
using AuctionService.Infrastructure.Messaging.Producers;
using AuctionService.Infrastructure.Persistence;
using AuctionService.Infrastructure.Persistence.Repositories;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection @this, IConfiguration configuration)
    {
        @this.AddDbContext<AuctionDbContext>(opt =>
        {
            opt.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(AuctionDbContext).Assembly.FullName));
        });
        
        return @this.Scan(x => x.FromAssemblyOf<AuctionDbContext>()
            .AddClasses(x => x.InNamespaceOf<AuctionRepository>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection @this, IConfiguration configuration)
    {
        @this.AddHostedService<AuctionBeginningMonitor>();
        @this.AddHostedService<AuctionHub>();
        
        @this.Configure<AuctionMonitorOptions>(configuration.GetSection(AuctionMonitorOptions.Section));
        
        return @this.Scan(x => x.FromAssemblyOf<IAuctionService>()
            .AddClasses(x => x.InExactNamespaceOf<Application.Services.AuctionService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(x => x.InExactNamespaceOf<AuctionHost>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }
    
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection @this, IConfiguration configuration)
    {
        @this.AddSingleton(_ => new BlobServiceClient(
            configuration.GetValue<string>("BlobServiceAccountConnectionString")));

        @this.AddScoped<IBlobService, BlobService>();
        @this.AddScoped<IProducer<ReadyToStartAuctionsMessage>, ReadyToStartAuctionsProducer>();


        @this.Configure<KafkaOptions>(configuration.GetSection(KafkaOptions.Section));

        return @this;
    }
}