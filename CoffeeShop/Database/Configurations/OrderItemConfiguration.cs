﻿using CoffeeShop.Entities.GroupOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.OrderItemId);

            builder.Property(oi => oi.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.Quantity)
                .IsRequired()
                .HasColumnType("int");

            builder.OwnsOne(oi => oi.Item, a =>
            {
                a.Property(io => io.CoffeeItemId)
                    .IsRequired()
                    .HasColumnType("int");

                a.Property(io => io.ItemName)
                    .IsRequired()
                    .HasColumnType("nvarchar(100)");

                a.Property(io => io.PictureUri)
                    .IsRequired()
                    .HasColumnType("nvarchar(265)");
            });
        }
    }
}
