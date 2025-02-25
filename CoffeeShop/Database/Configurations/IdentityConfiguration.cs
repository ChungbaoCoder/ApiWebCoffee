using CoffeeShop.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class IdentityConfiguration : IEntityTypeConfiguration<CustomerAuth>
{
    public void Configure(EntityTypeBuilder<CustomerAuth> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.BuyerId)
                .IsRequired()
                .HasColumnType("int");

        builder.Property(c => c.AspNetUserId)
                .IsRequired()
                .HasColumnType("nvarchar(450)");

        builder.Property(c => c.Password)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.HasOne(c => c.BuyerUser)
            .WithOne(b => b.CustomerAuth)
            .HasForeignKey<CustomerAuth>(c => c.BuyerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
