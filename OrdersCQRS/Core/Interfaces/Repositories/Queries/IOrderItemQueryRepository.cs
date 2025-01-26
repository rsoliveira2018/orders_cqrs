using Core.Entities;

namespace Core.Interfaces.Repositories.Queries;

public interface IOrderItemQueryRepository
{
    Task<IEnumerable<OrderItem>> GetAllAsync();
    Task<OrderItem> GetByIdAsync(int id);
    Task AddOrUpdateAsync(OrderItem orderItem);
    Task DeleteAsync(int id);
}
