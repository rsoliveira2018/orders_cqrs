using Core.Entities;
using Core.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class ProductReadRepository(IMongoDatabase mongoDatabase) : IProductReadRepository
{
    private readonly IMongoCollection<Product> _products = mongoDatabase.GetCollection<Product>("Products");

    public async Task<List<Product>> GetAllAsync()
    {
        return await _products.Find(_ => true).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _products.InsertOneAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
    }

    public async Task DeleteAsync(int id)
    {
        await _products.DeleteOneAsync(p => p.Id == id);
    }
}
