using CoffeeShop.Entities.GroupItem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace CoffeeShop.Database.Configurations;

public class CustomizationConfiguration : IEntityTypeConfiguration<Customization>
{
    public void Configure(EntityTypeBuilder<Customization> builder)
    {
        builder.Ignore(cu => cu.Topping);
        builder.Ignore(cu => cu.Flavor);

        builder.HasKey(cu => cu.CustomizationId);

        builder.HasOne(cu => cu.CoffeeItem)
            .WithOne(c => c.Customization)
            .HasForeignKey<Customization>(cu => cu.CoffeeItemId);

        builder.Property(cu => cu.MilkType)
            .IsRequired()
            .HasColumnType("nvarchar(50)");

        builder.Property(cu => cu.SugarLevel)
            .IsRequired()
            .HasColumnType("nvarchar(50)");

        builder.Property(cu => cu.Temperature)
            .IsRequired()
            .HasColumnType("nvarchar(50)");

        builder.Property(cu => cu._topping)
            .HasColumnType("nvarchar(200)");

        builder.Property(cu => cu._flavor)
            .HasColumnType("nvarchar(200)");
    }
}
