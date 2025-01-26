using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(AppDbContext appDbContext) : IProductRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<List<Product>> GetAllAsync()
    {
        return await _appDbContext.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _appDbContext.Products.FindAsync(id);
    }

    public async Task PostAsync(Product product)
    {
        await _appDbContext.Products.AddAsync(product);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task PutAsync(Product product)
    {
        _appDbContext.Products.Update(product);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _appDbContext.Products.FindAsync(id);
        if (product != null)
        {
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
