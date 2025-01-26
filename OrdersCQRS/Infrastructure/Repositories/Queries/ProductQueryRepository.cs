using Core.Entities;
using Core.Interfaces.Repositories.Queries;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Queries;

public class ProductQueryRepository(IMongoDatabase mongoDatabase) : IProductQueryRepository
{
    private readonly IMongoCollection<Product> _collection = mongoDatabase.GetCollection<Product>("Products");

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddOrUpdateAsync(Product product)
    {
        var existingProduct = await _collection.Find(p => p.Id == product.Id).FirstOrDefaultAsync();
        if (existingProduct == null)
        {
            await _collection.InsertOneAsync(product);
        }
        else
        {
            await _collection.ReplaceOneAsync(p => p.Id == product.Id, product);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _collection.DeleteOneAsync(p => p.Id == id);
    }
}
