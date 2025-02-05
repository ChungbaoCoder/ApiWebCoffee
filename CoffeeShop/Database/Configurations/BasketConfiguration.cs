using CoffeeShop.Entities.GroupBasket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.Ignore(b => b.Items);

        builder.HasKey(b => b.BasketId);

        builder.HasMany(b => b.Items)
            .WithOne(bi => bi.Basket)
            .HasForeignKey(bi => bi.BasketId);

        builder.Property(b => b.BuyerId)
            .IsRequired()
            .HasColumnType("int");
    }
}
