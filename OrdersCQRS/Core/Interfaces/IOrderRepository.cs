using Core.Entities;

namespace Core.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(Guid id);
    Task PostAsync(Order order);
    Task PutAsync(Order order);
    Task DeleteAsync(Guid id);
}
