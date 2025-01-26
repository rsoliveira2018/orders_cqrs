using Application.Dtos;
using Core.Entities;
using Core.Interfaces.Repositories.Services;
using Microsoft.AspNetCore.Mvc;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(int customerId, [FromBody] List<OrderItemDto> orderItemDtos)
    {
        var orderItemList = new List<OrderItem>();
        foreach(var orderItemDto in orderItemDtos)
        {
            OrderItem orderItem = new()
            {
                Quantity = orderItemDto.Quantity,
                ProductId = orderItemDto.ProductId,
                UnitPrice = orderItemDto.UnitPrice,
                TotalPrice = orderItemDto.Quantity * orderItemDto.UnitPrice
            };
            orderItemList.Add(orderItem);
        }

        try
        {
            var createdOrder = await _orderService.CreateOrderAsync(customerId, orderItemList);
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
