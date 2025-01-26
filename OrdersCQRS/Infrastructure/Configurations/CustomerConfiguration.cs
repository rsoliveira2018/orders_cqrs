using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .HasMany(c => c.Orders);

        builder.Property(c => c.Email).IsRequired();
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Phone).IsRequired();
    }
}