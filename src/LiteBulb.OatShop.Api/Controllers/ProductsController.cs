using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace LiteBulb.OatShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    // TODO: do mapping from Model to DTO in controller?

    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _productService;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    // GET: api/<ProductsController>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        _logger.LogDebug($"Entering controller method: {nameof(GetAllAsync)}");

        var response = await _productService.GetAsync();

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

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        _logger.LogDebug($"Entering controller method: {nameof(GetByIdAsync)}");

        if (id == 0)
        {
            return BadRequest("Id parameter cannot be 0.");
        }

        var response = await _productService.GetAsync(id);

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

    // POST api/<ProductsController>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Product product)
    {
        _logger.LogDebug($"Entering controller method: {nameof(CreateAsync)}");

        if (product is null)
        {
            return BadRequest("Product parameter cannot be null.");
        }

        if (product.Id != 0)
        {
            return BadRequest("Product.Id parameter cannot be null.");
        }

        var response = await _productService.AddAsync(product);

        if (response is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        if (response.HasErrors)
        {
            return Ok(response.ErrorMessage);
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

    // PUT api/<ProductsController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] Product product)
    {
        _logger.LogDebug($"Entering controller method: {nameof(UpdateAsync)}");

        if (id != product.Id)
            return BadRequest("Id field and Product.Id field do not match.");

        var response = await _productService.UpdateAsync(product);

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

    // DELETE api/<ProductsController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        _logger.LogDebug($"Entering controller method: {nameof(DeleteByIdAsync)}");

        if (id == 0)
        {
            return BadRequest("Id parameter cannot be 0.");
        }

        var response = await _productService.DeleteAsync(id);

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
