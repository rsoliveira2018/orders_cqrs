using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed;

public static class SeedData
{
    public static void Initialize(AppDbContext appDbContext)
    {
        appDbContext.Database.Migrate();

        if (!appDbContext.Customers.Any())
        {
            var customers = new List<Customer>
            {
                new() { Name = "Joelho Bichado", Email = "joelho_bichado@gmail.com", Phone = "(00) 900000000" },
                new() { Name = "Cotovelo Pontiagudo", Email = "cotovelo_pontiagudo@gmail.com", Phone = "(00) 900000000" },
            };

            appDbContext.Customers.AddRange(customers);
        }

        if (!appDbContext.Products.Any())
        {
            var products = new List<Product>
            {
                new() { Name = "Jujuba Carioca", Price = 2.39M },
                new() { Name = "Garrafa Invertida", Price = 75.29M },
                new() { Name = "Mala Sem Alça", Price = 548.99M }
            };

            appDbContext.Products.AddRange(products);
        }

        appDbContext.SaveChanges();
    }
}
