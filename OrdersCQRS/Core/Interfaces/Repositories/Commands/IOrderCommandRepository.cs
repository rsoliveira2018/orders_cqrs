using Core.Entities;

namespace Core.Interfaces.Repositories.Commands;

public interface IOrderCommandRepository
{
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(int id);
}
