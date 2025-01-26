using Core.Entities;

namespace Core.Interfaces;

public interface IOrderReadRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(int id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(int id);
}
