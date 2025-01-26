using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Commands;

public class OrderCommandRepository(AppDbContext appDbContext) : IOrderCommandRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddAsync(Order order)
    {
        await _appDbContext.Orders.AddAsync(order);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _appDbContext.Orders.Update(order);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var order = await _appDbContext.Orders.FindAsync(id);
        if (order != null)
        {
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
