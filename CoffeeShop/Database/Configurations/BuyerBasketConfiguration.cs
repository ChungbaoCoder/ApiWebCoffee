using CoffeeShop.Entities.GroupBasket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class BuyerBasketConfiguration : IEntityTypeConfiguration<BuyerBasket>
{
    public void Configure(EntityTypeBuilder<BuyerBasket> builder)
    {
        builder.HasKey(b => b.BasketId);

        builder.Property(bi => bi.BasketId)
            .ValueGeneratedOnAdd();

        builder.HasOne(b => b.Buyer)
            .WithOne(bu => bu.Baskets)
            .HasForeignKey<BuyerBasket>(b => b.BuyerId);

        builder.Property(b => b.BuyerId)
            .IsRequired(false)
            .HasColumnType("integer");
    }
}
