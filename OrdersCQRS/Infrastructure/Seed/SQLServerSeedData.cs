using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed;

public static class SQLServerSeedData
{
    public static void Initialize(AppDbContext appDbContext)
    {
        appDbContext.Database.Migrate();

        if (!appDbContext.Customers.Any())
            appDbContext.Customers.AddRange(SeedValues.CUSTOMERS);

        if (!appDbContext.Products.Any())
            appDbContext.Products.AddRange(SeedValues.PRODUCTS);

        appDbContext.SaveChanges();
    }
}
