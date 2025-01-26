using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Commands;

public class ProductCommandRepository(AppDbContext appDbContext) : IProductCommandRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddAsync(Product product)
    {
        await _appDbContext.Products.AddAsync(product);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _appDbContext.Products.Update(product);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _appDbContext.Products.FindAsync(id);
        if (product != null)
        {
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
