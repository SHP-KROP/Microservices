using AuctionService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionService.Infrastructure.Persistence.Configurations;

public class AuctionItemConfiguration : IEntityTypeConfiguration<AuctionItem>
{
    public void Configure(EntityTypeBuilder<AuctionItem> builder)
    {
        builder.HasKey(x => x.Id);
    }
}