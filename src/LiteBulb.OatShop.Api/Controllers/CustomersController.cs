using LiteBulb.OatShop.Application.Dtos;
using LiteBulb.OatShop.SharedKernel.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace LiteBulb.OatShop.Api.Controllers;
[Route("api/v1/[controller]")]
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

    /// <summary>
    /// Get list of all Customer objects from the database.
    /// </summary>
    /// <remarks>Returns 404 Not Found if list is empty</remarks>
    /// <example>GET api/v1/Customers</example>
    /// <response code="200" cref="Customer">Retrieved list of objects</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Get a Customer object by id field from database.
    /// </summary>
    /// <example>GET api/v1/Customers/5</example>
    /// <param name="id">Id of the Customer to retreive</param>
    /// <response code="200" cref="Customer">Retrieved object</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Add a new Customer object to database.
    /// </summary>
    /// <remarks>Id field must be null or 0.</remarks>
    /// <example>POST api/Customers</example>
    /// <param name="customer">Customer object to add (JSON)</param>
    /// <response code="201" cref="Customer">Created object</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Customer))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Update a Customer object in the database.
    /// </summary>
    /// <remarks>The id value in the route must match the Id field in the supplied Customer object</remarks>
    /// <example>PUT api/Customers/5</example>
    /// <param name="id">Id of the object to update</param>
    /// <param name="customer">Customer object to update (JSON)</param>
    /// <response code="200">Number of updated objects</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Delete a Customer object from the database.
    /// </summary>
    /// <example>DELETE api/Customers/5</example>
    /// <param name="id">Id of the object to delete</param>
    /// <response code="200">Number of deleted objects</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
