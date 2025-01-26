using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var schemaName = "orders_cqrs";
        modelBuilder.HasDefaultSchema(schemaName);

        modelBuilder.Entity<Customer>().ToTable(nameof(Customer), schemaName);
        modelBuilder.Entity<Order>().ToTable(nameof(Order), schemaName);
        modelBuilder.Entity<OrderItem>().ToTable(nameof(OrderItem), schemaName);
        modelBuilder.Entity<Product>().ToTable(nameof(Product), schemaName);

        // Apply configurations if any
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
