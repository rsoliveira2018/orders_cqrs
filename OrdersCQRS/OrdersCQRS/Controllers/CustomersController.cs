using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using OrdersCQRS.Handlers.Commands;
using OrdersCQRS.Handlers.Queries;

namespace OrdersCQRS.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController(CustomerCommandHandler commandHandler, CustomerQueryHandler queryHandler) : ControllerBase
{
    private readonly CustomerCommandHandler _commandHandler = commandHandler;
    private readonly CustomerQueryHandler _queryHandler = queryHandler;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
    {
        var customers = await _queryHandler.GetAllAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomerById(int id)
    {
        var customer = await _queryHandler.GetByIdAsync(id);

        if (customer == null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
    {
        await _commandHandler.AddAsync(customer);
        return CreatedAtAction(nameof(CreateCustomer), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
    {
        if (id != customer.Id) return BadRequest();
        await _commandHandler.UpdateAsync(customer);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        await _commandHandler.DeleteAsync(id);
        return NoContent();
    }
}
