using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.SharedKernel.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace LiteBulb.OatShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    // TODO: do mapping from Model to DTO in controller?

    private readonly ILogger<CustomersController> _logger;
    private readonly IService<Customer> _customerService;

    public CustomersController(ILogger<CustomersController> logger, IService<Customer> customerService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
    }

    // GET: api/<CustomersController>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        _logger.LogDebug($"Entering controller method: {nameof(GetAllAsync)}");

        var response = await _customerService.GetAsync();

        if (response is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (response.HasErrors)
        {
            return NotFound(response.ErrorMessage);
        }

        if (response.Result is null || response.Result.Count == 0)
        {
            return NotFound();
        }

        return Ok(response.Result);
    }

    // GET api/<CustomersController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        _logger.LogDebug($"Entering controller method: {nameof(GetByIdAsync)}");

        if (id == 0)
        {
            return BadRequest("Id parameter cannot be 0 for Find by Id.");
        }

        var response = await _customerService.GetAsync(id);

        if (response is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (response.HasErrors)
        {
            return NotFound(response.ErrorMessage);
        }

        if (response.Result is null)
        {
            return NotFound();
        }

        return Ok(response.Result);
    }

    // POST api/<CustomersController>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Customer customer)
    {
        _logger.LogDebug($"Entering controller method: {nameof(CreateAsync)}");

        if (customer is null)
        {
            return BadRequest("Customer parameter cannot be null for Create.");
        }

        if (customer.Id != 0)
        {
            return BadRequest("Customer.Id property must be 0 for Create.");
        }

        var response = await _customerService.AddAsync(customer);

        if (response is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (response.HasErrors)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, response.ErrorMessage);
        }

        if (response.Result is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return CreatedAtAction(
            actionName: nameof(GetByIdAsync),
            routeValues: new { response.Result.Id },
            value: response.Result);
    }

    // PUT api/<CustomersController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] Customer customer)
    {
        _logger.LogDebug($"Entering controller method: {nameof(UpdateAsync)}");

        if (id != customer.Id)
            return BadRequest("Id parameter and Customer.Id property must match for Update.");

        var response = await _customerService.UpdateAsync(customer);

        if (response is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (response.HasErrors)
        {
            return NotFound(response.ErrorMessage);
        }

        return Ok(response.Result);
    }

    // DELETE api/<CustomersController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        _logger.LogDebug($"Entering controller method: {nameof(DeleteByIdAsync)}");

        if (id == 0)
        {
            return BadRequest("Id parameter cannot be 0 for Delete by Id.");
        }

        var response = await _customerService.DeleteAsync(id);

        if (response is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (response.HasErrors)
        {
            return NotFound(response.ErrorMessage);
        }

        return Ok(response.Result);
    }
}
