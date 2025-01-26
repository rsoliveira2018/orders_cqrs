using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(AppDbContext appDbContext, IProductReadRepository mongoRepository) : IProductRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly IProductReadRepository _mongoRepository = mongoRepository;

    public async Task<List<Product>> GetAllAsync()
    {
        return await _appDbContext.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _appDbContext.Products.FindAsync(id);
    }

    public async Task AddAsync(Product product)
    {
        await _appDbContext.Products.AddAsync(product);
        await _appDbContext.SaveChangesAsync();

        await _mongoRepository.AddAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        _appDbContext.Products.Update(product);
        await _appDbContext.SaveChangesAsync();

        await _mongoRepository.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _appDbContext.Products.FindAsync(id);
        if (product != null)
        {
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();
        }

        var productOnMongo = await _mongoRepository.GetByIdAsync(id);
        if (productOnMongo != null)
        {
            await _mongoRepository.DeleteAsync(id);
        }
    }
}
