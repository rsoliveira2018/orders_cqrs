using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository(AppDbContext appDbContext, ICustomerReadRepository mongoRepository) : ICustomerRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly ICustomerReadRepository _mongoRepository = mongoRepository;

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _appDbContext.Customers.ToListAsync();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _appDbContext.Customers.FindAsync(id);
    }

    public async Task AddAsync(Customer customer)
    {
        await _appDbContext.Customers.AddAsync(customer);
        await _appDbContext.SaveChangesAsync();

        await _mongoRepository.AddAsync(customer);
    }

    public async Task UpdateAsync(Customer customer)
    {
        _appDbContext.Customers.Update(customer);
        await _appDbContext.SaveChangesAsync();

        await _mongoRepository.UpdateAsync(customer);
    }

    public async Task DeleteAsync(int id)
    {
        var customer = await _appDbContext.Customers.FindAsync(id);
        if (customer != null)
        {
            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync();
        }

        var productOnMongo = await _mongoRepository.GetByIdAsync(id);
        if (productOnMongo != null)
        {
            await _mongoRepository.DeleteAsync(id);
        }
    }
}
