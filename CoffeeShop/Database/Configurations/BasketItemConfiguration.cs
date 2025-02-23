using CoffeeShop.Entities.GroupBasket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.HasKey(bi => bi.BasketItemId);

        builder.Property(bi => bi.BasketItemId)
            .ValueGeneratedOnAdd();

        builder.HasOne(bi => bi.Basket)
            .WithMany(bb => bb.Items)
            .HasForeignKey(bi => bi.BasketId);

        builder.Property(bi => bi.ItemVariantId)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(bi => bi.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(bi => bi.Quantity)
            .IsRequired()
            .HasColumnType("int");
    }
}
