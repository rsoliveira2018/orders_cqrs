using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(
    ILogger<ProductsController> logger,
    IProductRepository productRepository,
    IProductReadRepository mongoRepository) : ControllerBase
{
    private readonly ILogger<ProductsController> _logger = logger;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductReadRepository _mongoRepository = mongoRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _mongoRepository.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _mongoRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        await _productRepository.AddAsync(product);
        return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        await _productRepository.UpdateAsync(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productRepository.DeleteAsync(id);
        return NoContent();
    }
}
