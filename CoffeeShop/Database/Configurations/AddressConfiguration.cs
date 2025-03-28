﻿using CoffeeShop.Entities.GroupBuyer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Database.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.AddressId);

        builder.Property(a => a.AddressId)
            .ValueGeneratedOnAdd();

        builder.HasOne(a => a.Buyer)
            .WithMany(b => b.Address)
            .HasForeignKey(a => a.BuyerId);

        builder.Property(a => a.Street)
            .IsRequired()
            .HasColumnType("nvarchar(200)");

        builder.Property(a => a.City)
            .IsRequired()
            .HasColumnType("nvarchar(200)");

        builder.Property(a => a.State)
            .IsRequired()
            .HasColumnType("nvarchar(200)");

        builder.Property(a => a.Country)
            .IsRequired()
            .HasColumnType("nvarchar(200)");

        builder.Property(a => a.IsDefault)
            .IsRequired()
            .HasColumnType("bit");
    }
}
