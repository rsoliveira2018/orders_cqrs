using Core.Entities;
using MongoDB.Driver;

namespace Infrastructure.Seed;

public static class MongoSeedData
{
    public static void Seed(IMongoDatabase database)
    {
        var productCollection = database.GetCollection<Product>("Products");
        var customerCollection = database.GetCollection<Customer>("Customers");

        if (!productCollection.AsQueryable().Any())
            productCollection.InsertMany(SeedValues.PRODUCTS);

        if (!customerCollection.AsQueryable().Any())
            customerCollection.InsertMany(SeedValues.CUSTOMERS);
    }
}
