using Core.Entities;

namespace Core.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(int customerId, List<OrderItem> orderItemsSent);
    Task<Order> GetOrderByIdAsync(int orderId);
}
