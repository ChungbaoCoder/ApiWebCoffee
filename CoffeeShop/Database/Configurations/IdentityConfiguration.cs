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

        builder.Property(c => c.Role)
            .HasConversion(
                v => v.ToString(),
                v => (UserRoleType)Enum.Parse(typeof(UserRoleType), v))
            .HasColumnType("varchar(20)");

        builder.HasOne(c => c.BuyerUser)
            .WithMany()
            .HasForeignKey(c => c.BuyerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.AspNetUser)
            .WithMany()
            .HasForeignKey(e => e.AspNetUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
