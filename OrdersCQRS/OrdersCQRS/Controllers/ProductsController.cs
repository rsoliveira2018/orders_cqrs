using Application.Commands;
using Application.Queries;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(ProductCommandHandler commandHandler, ProductQueryHandler queryHandler) : ControllerBase
{
    private readonly ProductCommandHandler _commandHandler = commandHandler;
    private readonly ProductQueryHandler _queryHandler = queryHandler;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _queryHandler.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _queryHandler.GetByIdAsync(id);
        
        if (product == null) 
            return NotFound();
        
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        await _commandHandler.AddAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id) return BadRequest();
        await _commandHandler.UpdateAsync(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _commandHandler.DeleteAsync(id);
        return NoContent();
    }
}
