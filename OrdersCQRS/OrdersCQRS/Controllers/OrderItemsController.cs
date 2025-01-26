using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderItemsController(
    ILogger<OrderItemsController> logger,
    IOrderItemRepository orderItemRepository,
    IOrderItemReadRepository mongoRepository) : ControllerBase
{
    private readonly ILogger<OrderItemsController> _logger = logger;
    private readonly IOrderItemRepository _orderItemRepository = orderItemRepository;
    private readonly IOrderItemReadRepository _mongoRepository = mongoRepository;

    [HttpGet]
    public ActionResult<IEnumerable<OrderItem>> GetOrderItemsByOrderId(int orderId)
    {
        var orderItems = _mongoRepository.GetOrderItemsByOrderId(orderId);
        return Ok(orderItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItem>> GetOrderItemById(int id)
    {
        var orderItem = await _mongoRepository.GetByIdAsync(id);
        if (orderItem == null)
            return NotFound();

        return Ok(orderItem);
    }

    [HttpPost]
    public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem orderItem)
    {
        await _orderItemRepository.AddAsync(orderItem);
        return CreatedAtAction(nameof(CreateOrderItem), new { id = orderItem.Id }, orderItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem(int id, OrderItem orderItem)
    {
        if (id != orderItem.Id)
            return BadRequest();

        await _orderItemRepository.UpdateAsync(orderItem);
        return NoContent();
    }

    [HttpDelete("{orderItemId}")]
    public async Task<IActionResult> DeleteOrderItem(int orderItemId)
    {
        await _orderItemRepository.DeleteAsync(orderItemId);
        return NoContent();
    }
}
