using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class OrderItemRepository(AppDbContext appDbContext, IOrderItemReadRepository mongoRepository) : IOrderItemRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly IOrderItemReadRepository _mongoRepository = mongoRepository;

    public List<OrderItem> GetOrderItemsByOrderId(int orderId)
    {
        var orderItems = _appDbContext.OrderItems.Where(oi => oi.OrderId == orderId).ToList();
        return orderItems;
    }

    public async Task<OrderItem> GetByIdAsync(int id)
    {
        return await _appDbContext.OrderItems.FindAsync(id);
    }

    public async Task AddAsync(OrderItem orderItem)
    {
        await _appDbContext.OrderItems.AddAsync(orderItem);
        await _appDbContext.SaveChangesAsync();

        await _mongoRepository.AddAsync(orderItem);
    }

    public async Task UpdateAsync(OrderItem orderItem)
    {
        _appDbContext.OrderItems.Update(orderItem);
        await _appDbContext.SaveChangesAsync();

        await _mongoRepository.UpdateAsync(orderItem);
    }

    public async Task DeleteAsync(int id)
    {
        var orderItem = await _appDbContext.OrderItems.FindAsync(id);
        if (orderItem != null)
        {
            _appDbContext.OrderItems.Remove(orderItem);
            await _appDbContext.SaveChangesAsync();
        }

        var productOnMongo = await _mongoRepository.GetByIdAsync(id);
        if (productOnMongo != null)
        {
            await _mongoRepository.DeleteAsync(id);
        }
    }
}
