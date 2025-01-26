using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using OrdersCQRS.Handlers.Commands;
using OrdersCQRS.Handlers.Queries;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderItemsController(OrderItemCommandHandler commandHandler, OrderItemQueryHandler queryHandler) : ControllerBase
{
    private readonly OrderItemCommandHandler _commandHandler = commandHandler;
    private readonly OrderItemQueryHandler _queryHandler = queryHandler;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetAllOrderItems()
    {
        var orderItems = await _queryHandler.GetAllAsync();
        return Ok(orderItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItem>> GetOrderItemById(int id)
    {
        var orderItem = await _queryHandler.GetByIdAsync(id);

        if (orderItem == null)
            return NotFound();

        return Ok(orderItem);
    }

    [HttpPost]
    public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem orderItem)
    {
        await _commandHandler.AddAsync(orderItem);
        return CreatedAtAction(nameof(CreateOrderItem), new { id = orderItem.Id }, orderItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem(int id, OrderItem orderItem)
    {
        if (id != orderItem.Id) return BadRequest();
        await _commandHandler.UpdateAsync(orderItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        await _commandHandler.DeleteAsync(id);
        return NoContent();
    }
}
