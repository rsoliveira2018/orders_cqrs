using Core.Entities;

namespace Core.Interfaces;

public interface IOrderItemReadRepository
{
    List<OrderItem> GetOrderItemsByOrderId(int orderId);
    Task<OrderItem> GetByIdAsync(int id);
    Task AddAsync(OrderItem orderItem);
    Task UpdateAsync(OrderItem orderItem);
    Task DeleteAsync(int id);
}
