using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Commands;

public class CustomerCommandRepository(AppDbContext appDbContext) : ICustomerCommandRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddAsync(Customer customer)
    {
        await _appDbContext.Customers.AddAsync(customer);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _appDbContext.Customers.Update(customer);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var customer = await _appDbContext.Customers.FindAsync(id);
        if (customer != null)
        {
            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
