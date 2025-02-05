using CoffeeShop.Entities.GroupBuyer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.Ignore(b => b.Address);
        builder.Ignore(b => b.Baskets);
        builder.Ignore(b => b.Order);

        builder.HasKey(b => b.BuyerId);

        builder.HasMany(b => b.Address)
            .WithOne(a => a.Buyer)
            .HasForeignKey(a => a.BuyerId);

        builder.HasMany(b => b.Order)
            .WithOne(a => a.Buyer)
            .HasForeignKey(a => a.BuyerId);

        builder.HasMany(b => b.Baskets)
            .WithOne(b => b.Buyer)
            .HasForeignKey(b => b.BuyerId);

        builder.Property(b => b.UserGuid)
            .HasColumnType("varchar(40)");

        builder.Property(b => b.Name)
            .IsRequired()
            .HasColumnType("nvarchar(100)");

        builder.Property(b => b.Email)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(b => b.DateCreated)
            .HasColumnType("datetime");

        builder.Property(b => b.DateUpdated)
            .HasColumnType("datetime");
    }
}
