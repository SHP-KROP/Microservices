using AuctionService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionService.Infrastructure.Persistence.Configurations;

public class AuctionItemPhotoConfiguration : IEntityTypeConfiguration<AuctionItemPhoto>
{
    public void Configure(EntityTypeBuilder<AuctionItemPhoto> builder)
    {
        builder.HasKey(x => x.Id);
    }
}