using Core.Entities;
using Core.Interfaces.Repositories.Queries;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Queries;

public class CustomerQueryRepository(IMongoDatabase mongoDatabase) : ICustomerQueryRepository
{
    private readonly IMongoCollection<Customer> _collection = mongoDatabase.GetCollection<Customer>("Customers");

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddOrUpdateAsync(Customer customer)
    {
        var existingCustomer = await _collection.Find(p => p.Id == customer.Id).FirstOrDefaultAsync();
        if (existingCustomer == null)
        {
            await _collection.InsertOneAsync(customer);
        }
        else
        {
            await _collection.ReplaceOneAsync(p => p.Id == customer.Id, customer);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _collection.DeleteOneAsync(p => p.Id == id);
    }
}
