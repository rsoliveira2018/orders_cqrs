using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController(
    ILogger<CustomersController> logger,
    ICustomerRepository customerRepository,
    ICustomerReadRepository mongoRepository) : ControllerBase
{
    private readonly ILogger<CustomersController> _logger = logger;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ICustomerReadRepository _mongoRepository = mongoRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
    {
        var customers = await _mongoRepository.GetAllAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomerById(int id)
    {
        var customer = await _mongoRepository.GetByIdAsync(id);
        if (customer == null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
    {
        await _customerRepository.AddAsync(customer);
        return CreatedAtAction(nameof(CreateCustomer), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
    {
        if (id != customer.Id)
            return BadRequest();

        await _customerRepository.UpdateAsync(customer);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        await _customerRepository.DeleteAsync(id);
        return NoContent();
    }
}
