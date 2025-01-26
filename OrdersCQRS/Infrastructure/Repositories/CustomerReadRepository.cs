using Core.Entities;
using Core.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class CustomerReadRepository(IMongoDatabase mongoDatabase) : ICustomerReadRepository
{
    private readonly IMongoCollection<Customer> _customers = mongoDatabase.GetCollection<Customer>("Customers");

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _customers.Find(_ => true).ToListAsync();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _customers.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Customer customer)
    {
        await _customers.InsertOneAsync(customer);
    }

    public async Task UpdateAsync(Customer customer)
    {
        await _customers.ReplaceOneAsync(p => p.Id == customer.Id, customer);
    }

    public async Task DeleteAsync(int id)
    {
        await _customers.DeleteOneAsync(p => p.Id == id);
    }
}
