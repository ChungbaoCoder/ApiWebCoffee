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

        builder.Property(ci => ci.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(ci => ci.Size)
            .IsRequired()
            .HasColumnType("nvarchar(50)");

        builder.Property(ci => ci.Category)
            .IsRequired()
            .HasColumnType("nvarchar(100)");

        builder.Property(ci => ci.PictureUri)
            .IsRequired()
            .HasColumnType("varchar(265)");

        builder.OwnsOne(ci => ci.Customization, a =>
        {
            a.Property(c => c.Option)
                .HasColumnType("nvarchar(100)");

            a.Property(c => c.Choices)
                .HasColumnType("nvarchar(100)");
        });

        builder.OwnsOne(ci => ci.Availability, a =>
        {
            a.Property(a => a.InStock)
                .IsRequired()
                .HasColumnType("bit");

            a.Property(a => a.NextBatchTime)
                .HasColumnType("datetime");
        });
    }
}
