using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiteBulb.OatShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;
    private readonly ICustomerService _customerService;

    public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
    }

    // GET: api/<CustomersController>
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var customers = await _customerService.GetAsync();

        if (customers is null || customers.Count == 0)
        {
            return NotFound();
        }

        return Ok(customers);
    }

    // GET api/<CustomersController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        if (id == 0)
        {
            return BadRequest("Id parameter cannot be 0.");
        }

        var customer = await _customerService.GetAsync(id);

        if (customer is null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    // POST api/<CustomersController>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Customer customer)
    {
        if (customer is null)
        {
            return BadRequest("Customer parameter cannot be null.");
        }

        if (customer.Id != 0)
        {
            return BadRequest("Customer.Id parameter cannot be null.");
        }

        var createdCustomer = await _customerService.AddAsync(customer);

        if (createdCustomer is null || createdCustomer.Id == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return CreatedAtAction(
            actionName: nameof(GetAsync),
            routeValues: new { createdCustomer.Id },
            value: createdCustomer);
    }

    // PUT api/<CustomersController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] Customer customer)
    {
        if (id != customer.Id)
            return BadRequest("Id field and Customer.Id field do not match.");

        var updatedCount = await _customerService.UpdateAsync(customer);

        if (updatedCount == 0)
            return NotFound();

        return Ok(updatedCount);
    }

    // DELETE api/<CustomersController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (id == 0)
        {
            return BadRequest("Id parameter cannot be 0.");
        }

        var deletedCount = await _customerService.DeleteAsync(id);

        if (deletedCount == 0)
            return NotFound();

        return Ok(deletedCount);
    }
}
