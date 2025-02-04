using CoffeeShop.Entities.GroupItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        //builder.HasKey(i => i.Id);

        //builder.Property(i => i.Name)
        //    .IsRequired()
        //    .HasMaxLength(100);

        //builder.Property(i => i.Price)
        //    .HasColumnType("decimal(18,2)");

        //builder.Property(i => i.Category)
        //    .IsRequired()
        //    .HasMaxLength(50);

        //builder.HasData(
        //    new Item { ItemId = 1, ItemName = "Espresso", ItemPrice = 2.50m, Category = "Espresso" },
        //    new Item { ItemId = 2, ItemName = "Latte", ItemPrice = 3.50m, Category = "Latte" }
        //);
    }
}
