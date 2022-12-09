using LiteBulb.OatShop.Domain.Dtos;
using LiteBulb.OatShop.Shared.Repositories;
using Microsoft.Extensions.Logging;

// TODO: refactor to be generic: CachedRepository<TId>

namespace LiteBulb.OatShop.Infrastructure.Repositories.Proxies;
public class CachedProductRepository : IRepository<Product, int>
{
    private readonly ILogger<CachedProductRepository> _logger;
    private readonly IRepository<Product, int> _productRepository;

    private readonly IDictionary<int, Product> _products;
    //private static IDictionary<int, Product> Products = new Concurrent.ConcurrentDictionary<int, Product>();

    public CachedProductRepository(ILogger<CachedProductRepository> logger, IRepository<Product, int> productRepository)
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

    public Task<int?> UpdateAsync(int id, Product model)
    {
        if (_products.ContainsKey(id))
        {
            _products[id] = model;
        }

        return _productRepository.UpdateAsync(id, model);
    }
}
