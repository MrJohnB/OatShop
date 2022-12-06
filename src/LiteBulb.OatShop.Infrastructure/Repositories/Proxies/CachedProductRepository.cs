using LiteBulb.OatShop.Domain.Dtos;
using LiteBulb.OatShop.Shared.Repositories;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Repositories.Proxies;
public class CachedProductRepository : IRepository<Product>
{
    private readonly ILogger<CachedProductRepository> _logger;
    private readonly IRepository<Product> _productRepository;

    private readonly IDictionary<int, Product> _products;
    //private static IDictionary<int, Product> Products = new Concurrent.ConcurrentDictionary<int, Product>();

    public CachedProductRepository(ILogger<CachedProductRepository> logger, IRepository<Product> productRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

        _products = new Dictionary<int, Product>();
    }

    public async Task<Product> AddAsync(Product model)
    {
        var product = await _productRepository.AddAsync(model);
        _products.TryAdd(product.Id, product);
        return product;
    }

    public Task<int?> DeleteAsync(int id)
    {
        _products.Remove(id); // TODO: return value if result if false?
        return _productRepository.DeleteAsync(id);
    }

    public Task<IReadOnlyList<Product>> GetAsync()
    {
        IReadOnlyList<Product> result = _products.Values.ToList(); // TODO: return the souce collection or a copy?
        return Task.FromResult(result);
    }

    public Task<Product?> GetAsync(int id)
    {
        if (_products.TryGetValue(id, out var product))
        {
            return Task.FromResult<Product?>(product);
        }

        return _productRepository.GetAsync(id);
    }

    public Task<int?> UpdateAsync(Product model)
    {
        if (_products.ContainsKey(model.Id))
        {
            _products[model.Id] = model;
        }

        return _productRepository.UpdateAsync(model);
    }
}
