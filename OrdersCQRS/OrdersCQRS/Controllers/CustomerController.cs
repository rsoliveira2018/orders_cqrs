using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController(ILogger<CustomerController> logger) : ControllerBase
{
    private readonly ILogger<CustomerController> _logger = logger;

    [HttpGet(Name = "GetCustomer")]
    public IEnumerable<Customer> Get()
    {
        _logger.LogInformation("Get customer endpoint has received a request");
        return Enumerable.Range(1, 5).Select(index => new Customer
        {
            Id = Guid.NewGuid(),
            Name = "Customer " + index,
            Email = "Email" + Random.Shared.Next(-20, 55) + "@gmail.com",
            Phone = PhoneNumbers[Random.Shared.Next(PhoneNumbers.Length)]
        })
        .ToArray();
    }

    private static readonly string[] PhoneNumbers =
    [
        "289383929", "485612387", "854512359", "100551589", "105055000"
    ];
}
