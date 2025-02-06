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

        builder.Property(cu => cu.Topping)
            .HasConversion(
                cv => JsonConvert.SerializeObject(cv ?? new List<string> { "none" }),
                cv => JsonConvert.DeserializeObject<List<string>> (cv) ?? new List<string> { "none" }
            );

        builder.Property(cu => cu.Flavor)
            .HasConversion(
                cv => JsonConvert.SerializeObject(cv ?? new List<string> { "none" }),
                cv => JsonConvert.DeserializeObject<List<string>>(cv) ?? new List<string> { "none" }
            );
    }
}
