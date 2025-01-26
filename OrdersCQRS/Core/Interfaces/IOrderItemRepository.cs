using Core.Entities;

namespace Core.Interfaces;

public interface IOrderItemRepository
{
    List<OrderItem> GetAllOrderItemsByOrderId(Guid orderId);
    Task<OrderItem> GetByIdAsync(Guid id);
    Task PostAsync(OrderItem orderItem);
    Task PutAsync(OrderItem orderItem);
    Task DeleteAsync(Guid id);
}
