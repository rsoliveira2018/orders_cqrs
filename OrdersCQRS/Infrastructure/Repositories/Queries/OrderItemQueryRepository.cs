using Core.Entities;
using Core.Interfaces.Repositories.Queries;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Queries;

public class OrderItemQueryRepository(IMongoDatabase mongoDatabase) : IOrderItemQueryRepository
{
    private readonly IMongoCollection<OrderItem> _collection = mongoDatabase.GetCollection<OrderItem>("OrderItems");

    public async Task<IEnumerable<OrderItem>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<OrderItem> GetByIdAsync(int id)
    {
        return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddOrUpdateAsync(OrderItem orderItem)
    {
        var existingOrderItem = await _collection.Find(p => p.Id == orderItem.Id).FirstOrDefaultAsync();
        if (existingOrderItem == null)
        {
            await _collection.InsertOneAsync(orderItem);
        }
        else
        {
            await _collection.ReplaceOneAsync(p => p.Id == orderItem.Id, orderItem);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _collection.DeleteOneAsync(p => p.Id == id);
    }
}
