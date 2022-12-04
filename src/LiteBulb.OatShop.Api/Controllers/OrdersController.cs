using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.SharedKernel.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace LiteBulb.OatShop.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    // TODO: do mapping from Model to DTO in controller?

    private readonly ILogger<OrdersController> _logger;
    private readonly IService<Order> _orderService;

    public OrdersController(ILogger<OrdersController> logger, IService<Order> orderService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    /// <summary>
    /// Get list of all Order objects from the database.
    /// </summary>
    /// <remarks>Returns 404 Not Found if list is empty</remarks>
    /// <example>GET api/v1/Orders</example>
    /// <response code="200" cref="Order">Retrieved list of objects</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync()
    {
        _logger.LogDebug($"Entering controller method: {nameof(GetAllAsync)}");

        var response = await _orderService.GetAsync();

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
    /// Get a Order object by id field from database.
    /// </summary>
    /// <example>GET api/v1/Orders/5</example>
    /// <param name="id">Id of the Order to retreive</param>
    /// <response code="200" cref="Order">Retrieved object</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        _logger.LogDebug($"Entering controller method: {nameof(GetByIdAsync)}");

        if (id == 0)
        {
            return BadRequest("Id parameter cannot be 0 for Get by Id.");
        }

        var response = await _orderService.GetAsync(id);

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
    /// Add a new Order object to database.
    /// </summary>
    /// <remarks>Id field must be null or 0.</remarks>
    /// <example>POST api/Orders</example>
    /// <param name="order">Order object to add (JSON)</param>
    /// <response code="201" cref="Order">Created object</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Order))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] Order order)
    {
        _logger.LogDebug($"Entering controller method: {nameof(CreateAsync)}");

        if (order is null)
        {
            return BadRequest("Order parameter cannot be null for Create.");
        }

        if (order.Id != 0)
        {
            return BadRequest("Order.Id property must be 0 for Create.");
        }

        var response = await _orderService.AddAsync(order);

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
    /// Update a Order object in the database.
    /// </summary>
    /// <remarks>The id value in the route must match the Id field in the supplied Order object</remarks>
    /// <example>PUT api/Orders/5</example>
    /// <param name="id">Id of the object to update</param>
    /// <param name="order">Order object to update (JSON)</param>
    /// <response code="200">Number of updated objects</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] Order order)
    {
        _logger.LogDebug($"Entering controller method: {nameof(UpdateAsync)}");

        if (id != order.Id)
            return BadRequest("Id parameter and Order.Id property must match for Update.");

        var response = await _orderService.UpdateAsync(order);

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
    /// Delete a Order object from the database.
    /// </summary>
    /// <example>DELETE api/Orders/5</example>
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

        var response = await _orderService.DeleteAsync(id);

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
