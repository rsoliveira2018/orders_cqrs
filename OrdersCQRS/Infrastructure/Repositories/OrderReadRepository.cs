using Core.Entities;
using Core.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class OrderReadRepository(IMongoDatabase mongoDatabase) : IOrderReadRepository
{
    private readonly IMongoCollection<Order> _orders = mongoDatabase.GetCollection<Order>("Orders");

    public async Task<List<Order>> GetAllAsync()
    {
        return await _orders.Find(_ => true).ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _orders.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Order order)
    {
        await _orders.InsertOneAsync(order);
    }

    public async Task UpdateAsync(Order order)
    {
        await _orders.ReplaceOneAsync(p => p.Id == order.Id, order);
    }

    public async Task DeleteAsync(int id)
    {
        await _orders.DeleteOneAsync(p => p.Id == id);
    }
}
