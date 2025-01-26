﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasMany(p => p.OrderItems);

        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
    }
}
