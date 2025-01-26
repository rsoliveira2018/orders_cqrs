using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class OrderItemRepository(AppDbContext appDbContext) : IOrderItemRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public List<OrderItem> GetAllOrderItemsByOrderId(Guid orderId)
    {
        var orderItems = _appDbContext.OrderItems.Where(oi => oi.OrderId == orderId).ToList();
        return orderItems;
    }

    public async Task<OrderItem> GetByIdAsync(Guid id)
    {
        return await _appDbContext.OrderItems.FindAsync(id);
    }

    public async Task PostAsync(OrderItem orderItem)
    {
        await _appDbContext.OrderItems.AddAsync(orderItem);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task PutAsync(OrderItem orderItem)
    {
        _appDbContext.OrderItems.Update(orderItem);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var orderItem = await _appDbContext.OrderItems.FindAsync(id);
        if (orderItem != null)
        {
            _appDbContext.OrderItems.Remove(orderItem);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
