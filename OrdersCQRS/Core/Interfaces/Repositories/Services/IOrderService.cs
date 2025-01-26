using Core.Entities;

namespace Core.Interfaces.Repositories.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(int customerId, List<OrderItem> orderItemsSent);
    Task<Order> GetOrderByIdAsync(int orderId);
}
