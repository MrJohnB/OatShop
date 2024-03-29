﻿using LiteBulb.OatShop.Domain.Dtos;
using LiteBulb.OatShop.Shared.Exceptions;
using LiteBulb.OatShop.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace LiteBulb.OatShop.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    // TODO: do mapping from Model to DTO in controller?

    private readonly ILogger<ProductsController> _logger;
    private readonly IService<Product, int> _productService;

    public ProductsController(ILogger<ProductsController> logger, IService<Product, int> productService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    /// <summary>
    /// Get list of all Product objects from the database.
    /// </summary>
    /// <remarks>Returns 404 Not Found if list is empty</remarks>
    /// <example>GET api/v1/Products</example>
    /// <returns>Product object collection</returns>
    /// <response code="200" cref="Product">Retrieved list of objects</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            return response.Exception switch
            {
                NotFoundException => NotFound(response.ErrorMessage),
                _ => StatusCode(StatusCodes.Status500InternalServerError, response.ErrorMessage)
            };
        }

        return Ok(response.Result);
    }

    /// <summary>
    /// Get a Product object by id field from database.
    /// </summary>
    /// <example>GET api/v1/Products/5</example>
    /// <param name="id">Id of the Product to retreive</param>
    /// <returns>Product object</returns>
    /// <response code="200" cref="Product">Retrieved object</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        _logger.LogDebug($"Entering controller method: {nameof(GetByIdAsync)}");

        var response = await _productService.GetAsync(id);

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
    /// Add a new Product object to database.
    /// </summary>
    /// <remarks>Do not set Id field to a non-default value.</remarks>
    /// <example>POST api/s</example>
    /// <param name="product">Product object to add (JSON)</param>
    /// <returns>Resource URI location of newly created object</returns>
    /// <response code="201" cref="Product">Created object</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] Product product)
    {
        _logger.LogDebug($"Entering controller method: {nameof(CreateAsync)}");

        if (product.Id is not null and not 0)
        {
            return BadRequest($"Product.Id property must be null (or absent) or {default(int)} for Create.");
        }

        var response = await _productService.AddAsync(product);

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
    /// Update a Product object in the database.
    /// </summary>
    /// <remarks>Do not set Product.Id field to a non-default value.</remarks>
    /// <example>PUT api/Products/5</example>
    /// <param name="id">Id of the object to update</param>
    /// <param name="product">Product object to update (JSON)</param>
    /// <returns>Number of updated objects</returns>
    /// <response code="200">Number of updated objects</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] Product product)
    {
        _logger.LogDebug($"Entering controller method: {nameof(UpdateAsync)}");

        if (product.Id is not null and not 0)
        {
            return BadRequest($"Product.Id property must be null (or absent) or {default(int)} for Update.");
        }

        var response = await _productService.UpdateAsync(id, product);

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
    /// Delete a Product object from the database.
    /// </summary>
    /// <example>DELETE api/Products/5</example>
    /// <param name="id">Id of the object to delete</param>
    /// <returns>Number of deleted objects</returns>
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

        var response = await _productService.DeleteAsync(id);

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
