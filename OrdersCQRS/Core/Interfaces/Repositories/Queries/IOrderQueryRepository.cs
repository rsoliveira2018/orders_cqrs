using Core.Entities;

namespace Core.Interfaces.Repositories.Queries;

public interface IOrderQueryRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(int id);
    Task AddOrUpdateAsync(Order order);
    Task DeleteAsync(int id);
}
