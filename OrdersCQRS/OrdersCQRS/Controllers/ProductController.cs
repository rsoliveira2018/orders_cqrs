using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(ILogger<ProductController> logger) : ControllerBase
{
    private readonly ILogger<ProductController> _logger = logger;

    [HttpGet(Name = "GetProduct")]
    public IEnumerable<Product> Get()
    {
        _logger.LogInformation("Get product endpoint has received a request");
        return Enumerable.Range(1, 5).Select(index => new Product
        {
            Id = Guid.NewGuid(),
            Name = "Product " + index,
            Price = Random.Shared.Next(10, 55) + index
        })
        .ToArray();
    }
}
