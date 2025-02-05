using CoffeeShop.Entities.GroupUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.Ignore(b => b.Address);

        builder.HasKey(b => b.BuyerId);

        builder.HasMany(b => b.Address)
            .WithOne(a => a.Buyer)
            .HasForeignKey(a => a.BuyerId);

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
