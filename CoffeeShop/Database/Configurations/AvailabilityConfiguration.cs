using CoffeeShop.Entities.GroupItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class AvailabilityConfiguration : IEntityTypeConfiguration<Availability>
{
    public void Configure(EntityTypeBuilder<Availability> builder)
    {
        builder.HasKey(a => a.AvailabilityId);

        builder.HasOne(a => a.CoffeeItem)
            .WithOne(c => c.Availability)
            .HasForeignKey<Availability>(a => a.CoffeeItemId);

        builder.Property(a => a.StockQuantity)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(a => a.AvailableStatus)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(a => a.RestockDate)
            .HasColumnType("datetime");
    }
}
