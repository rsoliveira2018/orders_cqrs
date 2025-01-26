using Core.Entities;

namespace Core.Interfaces.Repositories.Commands;

public interface ICustomerCommandRepository
{
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(int id);
}
