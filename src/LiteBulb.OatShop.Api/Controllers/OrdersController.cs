using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiteBulb.OatShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderService _orderService;

    public OrdersController(ILogger<OrdersController> logger, IOrderService orderService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    // GET: api/<OrdersController>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        _logger.LogDebug($"Entering controller method: {nameof(GetAllAsync)}");

        var orders = await _orderService.GetAsync();

        if (orders is null || orders.Count == 0)
        {
            return NotFound();
        }

        return Ok(orders);
    }

    // GET api/<OrdersController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        _logger.LogDebug($"Entering controller method: {nameof(GetByIdAsync)}");

        if (id == 0)
        {
            return BadRequest("Id parameter cannot be 0.");
        }

        var order = await _orderService.GetAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    // POST api/<OrdersController>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Order order)
    {
        _logger.LogDebug($"Entering controller method: {nameof(CreateAsync)}");

        if (order is null)
        {
            return BadRequest("Order parameter cannot be null.");
        }

        if (order.Id != 0)
        {
            return BadRequest("Order.Id parameter cannot be null.");
        }

        var createdOrder = await _orderService.AddAsync(order);

        if (createdOrder is null || createdOrder.Id == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return CreatedAtAction(
            actionName: nameof(GetByIdAsync),
            routeValues: new { createdOrder.Id },
            value: createdOrder);
    }

    // PUT api/<OrdersController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] Order order)
    {
        _logger.LogDebug($"Entering controller method: {nameof(UpdateAsync)}");

        if (id != order.Id)
            return BadRequest("Id field and Order.Id field do not match.");

        var updatedCount = await _orderService.UpdateAsync(order);

        if (updatedCount == 0)
            return NotFound();

        return Ok(updatedCount);
    }

    // DELETE api/<OrdersController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        _logger.LogDebug($"Entering controller method: {nameof(DeleteByIdAsync)}");

        if (id == 0)
        {
            return BadRequest("Id parameter cannot be 0.");
        }

        var deletedCount = await _orderService.DeleteAsync(id);

        if (deletedCount == 0)
            return NotFound();

        return Ok(deletedCount);
    }
}
