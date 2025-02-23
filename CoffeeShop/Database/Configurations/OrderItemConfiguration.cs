using CoffeeShop.Entities.GroupOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.OrderItemId);

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            builder.HasOne(oi => oi.ItemVariant)
                .WithMany(iv => iv.OrderItems)
                .HasForeignKey(oi => oi.ItemVariantId);

            builder.Property(oi => oi.ItemVariantId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(oi => oi.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.Quantity)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(oi => oi.OrderItemStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (OrderItemStatus)Enum.Parse(typeof(OrderItemStatus), v))
                .HasColumnType("varchar(20)");
        }
    }
}
