using AuctionService.Application.Services.Abstractions;
using AuctionService.Infrastructure.Persistence;
using AuctionService.Infrastructure.Persistence.Repositories;
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

    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection @this)
        => @this.Scan(x => x.FromAssemblyOf<IAuctionService>()
            .AddClasses(x => x.InNamespaceOf<Application.Services.AuctionService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
}