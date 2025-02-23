using CoffeeShop.Entities.GroupItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class ItemVariantConfiguration : IEntityTypeConfiguration<ItemVariant>
{
    public void Configure(EntityTypeBuilder<ItemVariant> builder)
    {
        builder.HasKey(iv => iv.ItemVariantId);

        builder.Property(iv => iv.ItemVariantId)
            .ValueGeneratedOnAdd();

        builder.HasOne(iv => iv.ProductItem)
            .WithMany(pi => pi.ItemVariant)
            .HasForeignKey(iv => iv.ProductId);

        builder.Property(iv => iv.ProductId)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(iv => iv.Size)
            .IsRequired()
            .HasColumnType("nvarchar(50)");

        builder.Property(iv => iv.StockQuantity)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(iv => iv.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(iv => iv.Status)
            .HasConversion(
                v => v.ToString(),
                v => (ItemStatus)Enum.Parse(typeof(ItemStatus), v))
            .HasColumnType("varchar(20)");

        builder.Property(iv => iv.DeletedAt)
            .IsRequired(false)
            .HasColumnType("datetime");
    }
}
