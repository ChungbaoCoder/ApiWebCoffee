using CoffeeShop.Entities.GroupItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.HasKey(pi => pi.ProductId);

        builder.Property(pi => pi.ProductId)
            .ValueGeneratedOnAdd();

        builder.Property(pi => pi.Name)
            .IsRequired()
            .HasColumnType("nvarchar(100)");

        builder.Property(pi => pi.Description)
            .IsRequired()
            .HasColumnType("nvarchar(200)");

        builder.Property(pi => pi.Category)
            .IsRequired()
            .HasColumnType("nvarchar(50)");

        builder.Property(pi => pi.ImageUri)
            .IsRequired()
            .HasColumnType("nvarchar(255)");

        builder.Property(pi => pi.CreatedDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(pi => pi.UpdatedDate)
            .IsRequired(false)
            .HasColumnType("datetime");

        builder.Property(pi => pi.DeletedAt)
            .IsRequired(false)
            .HasColumnType("datetime");
    }
}
