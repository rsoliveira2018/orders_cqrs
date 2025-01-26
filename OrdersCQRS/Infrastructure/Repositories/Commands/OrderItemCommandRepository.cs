using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Commands;

public class OrderItemCommandRepository(AppDbContext appDbContext) : IOrderItemCommandRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddAsync(OrderItem orderItem)
    {
        await _appDbContext.OrderItems.AddAsync(orderItem);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderItem orderItem)
    {
        _appDbContext.OrderItems.Update(orderItem);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var orderItem = await _appDbContext.OrderItems.FindAsync(id);
        if (orderItem != null)
        {
            _appDbContext.OrderItems.Remove(orderItem);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
