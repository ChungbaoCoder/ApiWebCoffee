using CoffeeShop.Entities.GroupBuyer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class BuyerConfiguration : IEntityTypeConfiguration<BuyerUser>
{
    public void Configure(EntityTypeBuilder<BuyerUser> builder)
    {
        builder.HasKey(b => b.BuyerId);

        builder.Property(bi => bi.BuyerId)
            .ValueGeneratedOnAdd();

        builder.HasIndex(b => b.Email)
            .IsUnique();

        builder.Property(b => b.Name)
            .IsRequired()
            .HasColumnType("nvarchar(150)");

        builder.Property(b => b.Email)
            .IsRequired()
            .HasColumnType("nvarchar(150)");

        builder.Property(b => b.PhoneNum)
            .IsRequired()
            .HasColumnType("varchar(20)");

        builder.Property(b => b.DateJoined)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(b => b.DeletedAt)
            .IsRequired(false)
            .HasColumnType("datetime");
    }
}
