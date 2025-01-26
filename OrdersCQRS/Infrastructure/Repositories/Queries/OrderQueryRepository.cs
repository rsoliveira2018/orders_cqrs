using Core.Entities;
using Core.Interfaces.Repositories.Queries;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Queries;

public class OrderQueryRepository(IMongoDatabase mongoDatabase) : IOrderQueryRepository
{
    private readonly IMongoCollection<Order> _collection = mongoDatabase.GetCollection<Order>("Orders");

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddOrUpdateAsync(Order order)
    {
        var existingOrder = await _collection.Find(p => p.Id == order.Id).FirstOrDefaultAsync();
        if (existingOrder == null)
        {
            await _collection.InsertOneAsync(order);
        }
        else
        {
            await _collection.ReplaceOneAsync(p => p.Id == order.Id, order);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _collection.DeleteOneAsync(p => p.Id == id);
    }
}
