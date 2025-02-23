using CoffeeShop.Entities.GroupOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class BuyerOrderConfiguration : IEntityTypeConfiguration<BuyerOrder>
{
    public void Configure(EntityTypeBuilder<BuyerOrder> builder)
    {
        builder.HasKey(o => o.OrderId);

        builder.HasOne(o => o.Buyer)
            .WithMany(b => b.Orders)
            .HasForeignKey(o => o.BuyerId);

        builder.Property(o => o.BuyerId)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(o => o.OrderedDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(o => o.Total)
            .IsRequired()
            .HasColumnType("decimal(18.2)");

        builder.Property(o => o.OrderStatus)
            .HasConversion(
                v => v.ToString(),
                v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v))
            .HasColumnType("varchar(20)");

        builder.Property(o => o.PaymentStatus)
            .HasConversion(
                v => v.ToString(),
                v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v))
            .HasColumnType("varchar(20)");

        builder.OwnsOne(o => o.Address, a =>
        {
            a.Property(o => o.Street)
                .IsRequired()
                .HasColumnType("nvarchar(200)");
            a.Property(o => o.City)
                .IsRequired()
                .HasColumnType("nvarchar(200)");
            a.Property(o => o.State)
                .IsRequired()
                .HasColumnType("nvarchar(200)");
            a.Property(o => o.Country)
                .IsRequired()
                .HasColumnType("nvarchar(200)");
        });

        builder.Property(o => o.UpdatedDate)
            .HasColumnType("datetime");

        builder.Property(o => o.DeletedAt)
            .IsRequired(false)
            .HasColumnType("datetime");
    }
}
