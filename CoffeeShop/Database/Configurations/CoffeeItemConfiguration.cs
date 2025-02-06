using CoffeeShop.Entities.GroupItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class CoffeeItemConfiguration : IEntityTypeConfiguration<CoffeeItem>
{
    public void Configure(EntityTypeBuilder<CoffeeItem> builder)
    {
        builder.HasKey(ci => ci.CoffeeItemId);

        builder.Property(ci => ci.Name)
            .IsRequired()
            .HasColumnType("nvarchar(100)");

        builder.Property(ci => ci.Description)
            .IsRequired()
            .HasColumnType("nvarchar(200)");

        builder.Property(ci => ci.Category)
            .IsRequired()
            .HasColumnType("nvarchar(50)");

        builder.Property(ci => ci.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(ci => ci.Size)
            .IsRequired()
            .HasColumnType("nvarchar(50)");

        builder.Property(ci => ci.PictureUri)
            .IsRequired()
            .HasColumnType("nvarchar(255)");
    }
}
