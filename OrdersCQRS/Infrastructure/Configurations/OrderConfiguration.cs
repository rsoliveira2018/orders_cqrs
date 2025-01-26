using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

        builder.Property(o => o.CustomerId).IsRequired();
        builder.Property(o => o.OrderDate).IsRequired();
        builder.Property(o => o.Status).IsRequired();
        builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
    }
}