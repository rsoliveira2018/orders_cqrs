using Core.Entities;
using Core.Interfaces;

namespace Core.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    ICustomerRepository customerRepository)
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<Order> CreateOrderAsync(int customerId, List<OrderItem> orderItemsSent)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId) ?? throw new KeyNotFoundException($"Customer with ID {customerId} not found.");

        var orderItems = new List<OrderItem>();
        foreach (var orderItemSent in orderItemsSent)
        {
            var product = await _productRepository.GetByIdAsync(orderItemSent.ProductId) ?? throw new KeyNotFoundException($"Product with ID {orderItemSent.ProductId} not found.");
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

        await _orderRepository.AddAsync(order);
        return order;
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId) ?? throw new KeyNotFoundException($"Order with Id {orderId} not found.");
        return order;
    }
}
