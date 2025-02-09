using CoffeeShop.Entities.GroupOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class BuyerOrderConfiguration : IEntityTypeConfiguration<BuyerOrder>
{
    public void Configure(EntityTypeBuilder<BuyerOrder> builder)
    {
        builder.Ignore(o => o.OrderItems);

        builder.HasKey(o => o.OrderId);

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);

        builder.Property(o => o.CreatedDate)
            .HasColumnType("datetime");

        builder.Property(o => o.UpdatedDate)
            .HasColumnType("datetime");

        builder.Property(o => o.Total)
            .HasColumnType("decimal(18.2)");

        builder.OwnsOne(o => o.ShipAddress, a =>
        {
            a.Property(sa => sa.Street)
                .IsRequired()
                .HasColumnType("nvarchar(200)");

            a.Property(sa => sa.City)
                .IsRequired()
                .HasColumnType("nvarchar(200)");

            a.Property(sa => sa.State)
                .IsRequired()
                .HasColumnType("nvarchar(200)");

            a.Property(sa => sa.Country)
                .IsRequired()
                .HasColumnType("nvarchar(200)");
        });

        builder.OwnsOne(o => o.Status, a =>
        {
            a.Property(s => s.Status)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            a.Property(s => s.CompleteTime)
                .IsRequired()
                .HasColumnType("datetime");

            a.Property(s => s.LastUpdate)
                .HasColumnType("datetime");
        });
    }
}
