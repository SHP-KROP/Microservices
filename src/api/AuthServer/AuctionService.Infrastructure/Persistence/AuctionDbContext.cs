using System.Reflection;
using AuctionService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Infrastructure.Persistence;

public sealed class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
    {
        
    }

    public DbSet<Auction> Auctions { get; set; }

    public DbSet<AuctionItem> AuctionItems { get; set; }

    public DbSet<AuctionItemPhoto> AuctionItemPhotos { get; set; }

    public DbSet<Bid> Bids { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("auction");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}