using Core.Entities;

namespace Core.Interfaces.Repositories.Commands;

public interface IOrderItemCommandRepository
{
    Task AddAsync(OrderItem orderItem);
    Task UpdateAsync(OrderItem orderItem);
    Task DeleteAsync(int id);
}
