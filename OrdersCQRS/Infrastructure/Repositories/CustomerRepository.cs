using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository(AppDbContext appDbContext) : ICustomerRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _appDbContext.Customers.ToListAsync();
    }

    public async Task<Customer> GetByIdAsync(Guid id)
    {
        return await _appDbContext.Customers.FindAsync(id);
    }

    public async Task PostAsync(Customer customer)
    {
        await _appDbContext.Customers.AddAsync(customer);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task PutAsync(Customer customer)
    {
        _appDbContext.Customers.Update(customer);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var customer = await _appDbContext.Customers.FindAsync(id);
        if (customer != null)
        {
            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
