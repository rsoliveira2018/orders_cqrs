using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(ILogger<OrderController> logger) : ControllerBase
{
    private readonly ILogger<OrderController> _logger = logger;

    [HttpGet(Name = "GetOrder")]
    public IEnumerable<Order> Get()
    {
        _logger.LogInformation("Get order endpoint has received a request");
        return Enumerable.Range(1, 5).Select(index => new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            OrderDate = DateTime.Now.AddDays(index),
            TotalAmount = Random.Shared.Next(-20, 55) * index,
            Status = (OrderStatus)Random.Shared.Next(Enum.GetValues(typeof(OrderStatus)).Length)
        })
        .ToArray();
    }
}
