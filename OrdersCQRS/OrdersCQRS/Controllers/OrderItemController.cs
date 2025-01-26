using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderItemController(ILogger<OrderItemController> logger) : ControllerBase
{
    private readonly ILogger<OrderItemController> _logger = logger;

    [HttpGet(Name = "GetOrderItem")]
    public IEnumerable<OrderItem> Get()
    {
        _logger.LogInformation("Get order item endpoint has received a request");

        return Enumerable.Range(1, 5).Select(index => new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductName = "Product " + index,
            Quantity = index,
            UnitPrice = Random.Shared.Next(1, 15) * index,
            TotalPrice = Random.Shared.Next(30, 195) * index
        })
        .ToArray();
    }
}
