using LiteBulb.OatShop.Domain.Dtos;
using LiteBulb.OatShop.Shared.Exceptions;
using LiteBulb.OatShop.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace LiteBulb.OatShop.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    // TODO: do mapping from Model to DTO in controller?

    private readonly ILogger<CustomersController> _logger;
    private readonly IService<Customer, int> _customerService;

    public CustomersController(ILogger<CustomersController> logger, IService<Customer, int> customerService)
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
            return response.Exception switch
            {
                NotFoundException => NotFound(response.ErrorMessage),
                _ => StatusCode(StatusCodes.Status500InternalServerError, response.ErrorMessage)
            };
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

        var response = await _customerService.GetAsync(id);

        if (response is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (response.HasErrors)
        {
            return response.Exception switch
            {
                BadRequestException => BadRequest(response.ErrorMessage),
                NotFoundException => NotFound(response.ErrorMessage),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        return Ok(response.Result);
    }

    /// <summary>
    /// Add a new Customer object to database.
    /// </summary>
    /// <remarks>Do not set Id field to a non-default value.</remarks>
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

        if (customer.Id is not null and not 0)
        {
            return BadRequest($"Customer.Id property must be null (or absent) or {default(int)} for Create.");
        }

        var response = await _customerService.AddAsync(customer);

        if (response is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (response.HasErrors)
        {
            return response.Exception switch
            {
                BadRequestException => BadRequest(response.ErrorMessage),
                _ => StatusCode(StatusCodes.Status500InternalServerError, response.ErrorMessage)
            };
        }

        return CreatedAtAction(
            actionName: nameof(GetByIdAsync),
            routeValues: new { response.Result?.Id },
            value: response.Result);
    }

    /// <summary>
    /// Update a Customer object in the database.
    /// </summary>
    /// <remarks>Do not set Customer.Id field to a non-default value.</remarks>
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

        if (customer.Id is not null and not 0)
        {
            return BadRequest($"Customer.Id property must be null (or absent) or {default(int)} for Update.");
        }

        var response = await _customerService.UpdateAsync(id, customer);

        if (response is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (response.HasErrors)
        {
            return response.Exception switch
            {
                BadRequestException => BadRequest(response.ErrorMessage),
                NotFoundException => NotFound(response.ErrorMessage),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
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

        var response = await _customerService.DeleteAsync(id);

        if (response is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (response.HasErrors)
        {
            return response.Exception switch
            {
                BadRequestException => BadRequest(response.ErrorMessage),
                NotFoundException => NotFound(response.ErrorMessage),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        return Ok(response.Result);
    }
}
