using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository(AppDbContext appDbContext) : IOrderRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<List<Order>> GetAllAsync()
    {
        return await _appDbContext.Orders.ToListAsync();
    }

    public async Task<Order> GetByIdAsync(Guid id)
    {
        return await _appDbContext.Orders.FindAsync(id);
    }

    public async Task PostAsync(Order order)
    {
        await _appDbContext.Orders.AddAsync(order);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task PutAsync(Order order)
    {
        _appDbContext.Orders.Update(order);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await _appDbContext.Orders.FindAsync(id);
        if (order != null)
        {
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
