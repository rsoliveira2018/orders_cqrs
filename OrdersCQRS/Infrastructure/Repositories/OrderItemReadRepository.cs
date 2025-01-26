using Core.Entities;
using Core.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class OrderItemReadRepository(IMongoDatabase mongoDatabase) : IOrderItemReadRepository
{
    private readonly IMongoCollection<OrderItem> _orderItems = mongoDatabase.GetCollection<OrderItem>("OrderItems");

    public async Task<OrderItem> GetByIdAsync(int id)
    {
        return await _orderItems.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(OrderItem orderItem)
    {
        await _orderItems.InsertOneAsync(orderItem);
    }

    public async Task UpdateAsync(OrderItem orderItem)
    {
        await _orderItems.ReplaceOneAsync(p => p.Id == orderItem.Id, orderItem);
    }

    public async Task DeleteAsync(int id)
    {
        await _orderItems.DeleteOneAsync(p => p.Id == id);
    }

    public List<OrderItem> GetOrderItemsByOrderId(int orderId)
    {
        return _orderItems.Find(oi => oi.OrderId == orderId).ToList();
    }
}
