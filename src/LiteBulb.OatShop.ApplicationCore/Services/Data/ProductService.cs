using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Responses;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Repositories.Data;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Data;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.ApplicationCore.Services.Data;
public class ProductService : IService<Product>
{
    private readonly ILogger<ProductService> _logger;
    private readonly IRepository<Product> _productRepository;

    public ProductService(ILogger<ProductService> logger, IRepository<Product> productRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ServiceResponse<ICollection<Product>>> GetAsync()
    {
        var result = await _productRepository.GetAsync();

        if (result is null)
        {
            return new ServiceResponse<ICollection<Product>>(true,
                "Error occurred while retrieving list of Product objects.  Result was null for some reason.");
        }

        return new ServiceResponse<ICollection<Product>>(result);
    }

    public async Task<ServiceResponse<Product>> GetAsync(int id)
    {
        var result = await _productRepository.GetAsync(id);

        if (result is null)
        {
            return new ServiceResponse<Product>(true,
                $"Error occurred while retrieving Product object with id '{id}'.  Product object was not found in the database.");
        }

        return new ServiceResponse<Product>(result);
    }

    public async Task<ServiceResponse<Product>> AddAsync(Product product)
    {
        var result = await _productRepository.AddAsync(product);

        if (result is null)
        {
            return new ServiceResponse<Product>(true,
                "Error occurred while adding a Product object to database.  Result returned by add process was null for some reason.");
        }

        if (result.Id < 1)
        {
            return new ServiceResponse<Product>(true,
                $"Error occurred while adding a Product object to database.  Result returned by add process has an id of {result.Id} which is invalid.");
        }

        return new ServiceResponse<Product>(result);
    }

    public async Task<ServiceResponse<int>> UpdateAsync(Product product)
    {
        var affectedCount = await _productRepository.UpdateAsync(product);

        if (affectedCount is null || !affectedCount.HasValue)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while updating a Product object in the database.  Product object with id '{product.Id}' was not found in the database.");
        }

        if (affectedCount < 1)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while updating a Product object in the database.  The affected record count is '{affectedCount}' which is invalid.");
        }

        return new ServiceResponse<int>(affectedCount.Value);
    }

    public async Task<ServiceResponse<int>> DeleteAsync(int id)
    {
        var affectedCount = await _productRepository.DeleteAsync(id);

        if (affectedCount is null || !affectedCount.HasValue)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while deleting a Product object from the database.  Product object with id '{id}' was not found in the database.");
        }

        if (affectedCount < 1)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while deleting a Product object from the database.  The affected record count is '{affectedCount}' which is invalid.");
        }

        return new ServiceResponse<int>(affectedCount.Value);
    }
}
