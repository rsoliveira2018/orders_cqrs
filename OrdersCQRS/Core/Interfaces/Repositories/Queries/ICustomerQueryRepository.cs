using Core.Entities;

namespace Core.Interfaces.Repositories.Queries;

public interface ICustomerQueryRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> GetByIdAsync(int id);
    Task AddOrUpdateAsync(Customer customer);
    Task DeleteAsync(int id);
}
