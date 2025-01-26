using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository(AppDbContext appDbContext, IOrderReadRepository mongoRepository) : IOrderRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly IOrderReadRepository _mongoRepository = mongoRepository;

    public async Task<List<Order>> GetAllAsync()
    {
        return await _appDbContext.Orders.ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _appDbContext.Orders.FindAsync(id);
    }

    public async Task AddAsync(Order order)
    {
        await _appDbContext.Orders.AddAsync(order);
        await _appDbContext.SaveChangesAsync();

        await _mongoRepository.AddAsync(order);
    }

    public async Task UpdateAsync(Order order)
    {
        _appDbContext.Orders.Update(order);
        await _appDbContext.SaveChangesAsync();

        await _mongoRepository.UpdateAsync(order);
    }

    public async Task DeleteAsync(int id)
    {
        var order = await _appDbContext.Orders.FindAsync(id);
        if (order != null)
        {
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync();
        }

        var productOnMongo = await _mongoRepository.GetByIdAsync(id);
        if (productOnMongo != null)
        {
            await _mongoRepository.DeleteAsync(id);
        }
    }
}
