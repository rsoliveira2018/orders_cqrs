using Core.Entities;

namespace Core.Interfaces;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer> GetByIdAsync(Guid id);
    Task PostAsync(Customer customer);
    Task PutAsync(Customer customer);
    Task DeleteAsync(Guid id);
}
