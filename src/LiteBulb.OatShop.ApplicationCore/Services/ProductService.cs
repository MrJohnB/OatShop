using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Repositories;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.ApplicationCore.Services;
public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly IProductRepository _productRepository;

    public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ICollection<Product>> GetAsync()
    {
        return await _productRepository.GetAsync();
    }

    public async Task<Product?> GetAsync(int id)
    {
        return await _productRepository.GetAsync(id);
    }

    public async Task<Product> AddAsync(Product dto)
    {
        return await _productRepository.AddAsync(dto);
    }

    public async Task<int> UpdateAsync(Product dto)
    {
        return await _productRepository.UpdateAsync(dto);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _productRepository.DeleteAsync(id);
    }
}
