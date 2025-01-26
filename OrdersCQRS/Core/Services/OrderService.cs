using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Core.Interfaces.Repositories.Queries;
using Core.Interfaces.Repositories.Services;

namespace Core.Services;

public class OrderService(
    IOrderCommandRepository orderCommandRepository,
    IOrderQueryRepository orderQueryRepository,
    IProductQueryRepository productQueryRepository,
    ICustomerQueryRepository customerQueryRepository) : IOrderService
{
    private readonly IOrderCommandRepository _orderCommandRepository = orderCommandRepository;
    private readonly IOrderQueryRepository _orderQueryRepository = orderQueryRepository;
    private readonly IProductQueryRepository _productQueryRepository = productQueryRepository;
    private readonly ICustomerQueryRepository _customerQueryRepository = customerQueryRepository;

    public async Task<Order> CreateOrderAsync(int customerId, List<OrderItem> orderItemsSent)
    {
        var customer = await _customerQueryRepository.GetByIdAsync(customerId) ?? throw new KeyNotFoundException($"Customer with ID {customerId} not found.");

        var orderItems = new List<OrderItem>();
        foreach (var orderItemSent in orderItemsSent)
        {
            var product = await _productQueryRepository.GetByIdAsync(orderItemSent.ProductId) ?? throw new KeyNotFoundException($"Product with ID {orderItemSent.ProductId} not found.");
            OrderItem orderItem = new()
            {
                ProductId = product.Id,
                Product = product,
                UnitPrice = product.Price,
                Quantity = orderItemSent.Quantity,
                TotalPrice = orderItemSent.Quantity * product.Price
            };
            orderItems.Add(orderItem);
        }

        Order order = new()
        {
            Customer = customer,
            OrderItems = orderItems,
            OrderDate = DateTime.UtcNow
        };

        await _orderCommandRepository.AddAsync(order);
        return order;
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderQueryRepository.GetByIdAsync(orderId) ?? throw new KeyNotFoundException($"Order with Id {orderId} not found.");
        return order;
    }
}
