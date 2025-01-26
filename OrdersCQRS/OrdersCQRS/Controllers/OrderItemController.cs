using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderItemController(ILogger<OrderItemController> logger, IOrderItemRepository orderItemRepository) : ControllerBase
{
    private readonly ILogger<OrderItemController> _logger = logger;
    private readonly IOrderItemRepository _orderItemRepository = orderItemRepository;

    [HttpGet]
    public ActionResult<IEnumerable<OrderItem>> GetAllOrderItems(Guid orderId)
    {
        var orderItems = _orderItemRepository.GetAllOrderItemsByOrderId(orderId);
        return Ok(orderItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItem>> GetOrderItemById(Guid id)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(id);
        if (orderItem == null)
            return NotFound();

        return Ok(orderItem);
    }

    [HttpPost]
    public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem orderItem)
    {
        await _orderItemRepository.PostAsync(orderItem);
        return CreatedAtAction(nameof(CreateOrderItem), new { id = orderItem.Id }, orderItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem(Guid id, OrderItem orderItem)
    {
        if (id != orderItem.Id)
            return BadRequest();

        await _orderItemRepository.PutAsync(orderItem);
        return NoContent();
    }

    [HttpDelete("{orderItemId}")]
    public async Task<IActionResult> DeleteOrderItem(Guid orderItemId)
    {
        await _orderItemRepository.DeleteAsync(orderItemId);
        return NoContent();
    }
}
