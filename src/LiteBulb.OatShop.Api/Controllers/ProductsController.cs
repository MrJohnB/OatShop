using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiteBulb.OatShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
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

        var products = await _productService.GetAsync();

        if (products is null || products.Count == 0)
        {
            return NotFound();
        }

        return Ok(products);
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

        var product = await _productService.GetAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
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

        var createdProduct = await _productService.AddAsync(product);

        if (createdProduct is null || createdProduct.Id == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return CreatedAtAction(
            actionName: nameof(GetByIdAsync),
            routeValues: new { createdProduct.Id },
            value: createdProduct);
    }

    // PUT api/<ProductsController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] Product product)
    {
        _logger.LogDebug($"Entering controller method: {nameof(UpdateAsync)}");

        if (id != product.Id)
            return BadRequest("Id field and Product.Id field do not match.");

        var updatedCount = await _productService.UpdateAsync(product);

        if (updatedCount == 0)
            return NotFound();

        return Ok(updatedCount);
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

        var deletedCount = await _productService.DeleteAsync(id);

        if (deletedCount == 0)
            return NotFound();

        return Ok(deletedCount);
    }
}
