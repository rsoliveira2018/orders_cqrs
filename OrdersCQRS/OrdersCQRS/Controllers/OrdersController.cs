using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController(
    ILogger<OrdersController> logger,
    IOrderService orderService) : ControllerBase
{
    private readonly ILogger<OrdersController> _logger = logger;
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(int customerId, [FromBody] List<OrderItem> orderItems)
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
    public async Task<ActionResult<Order>> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }
}
