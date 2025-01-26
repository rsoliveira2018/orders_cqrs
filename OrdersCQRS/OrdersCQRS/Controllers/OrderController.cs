using Core.Entities;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(ILogger<OrderController> logger, OrderService orderService) : ControllerBase
{
    private readonly ILogger<OrderController> _logger = logger;
    private readonly OrderService _orderService = orderService;

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Guid customerId, [FromBody] List<OrderItem> orderItems)
    {
        try
        {
            var createdOrder = await _orderService.CreateOrderAsync(customerId, orderItems);
            return CreatedAtAction(nameof(CreateOrder), new { id = createdOrder.Id }, createdOrder);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderById(Guid id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }
}
