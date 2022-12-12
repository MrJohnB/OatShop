using LiteBulb.OatShop.Shared.Entities;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Shared.Repositories;
/// <summary>
/// This is a proxy to the Repository that stores a cache of the items in a Dictionary.
/// <example>
/// IOC container service registration:
/// <code>
/// .AddScoped<IRepository<Product, int>>(serviceProvider =>
///     new CachedRepository<Product, int>(
///         logger: serviceProvider.GetRequiredService<ILogger<ProductRepository>>(),
///         repository: new ProductRepository(
///             serviceProvider.GetRequiredService<ILogger<ProductRepository>>(),
///             serviceProvider.GetRequiredService<OatShopDbContext>(),
///             serviceProvider.GetRequiredService<IMapper<Entities.Product, Product>>())));
/// </code>
/// </example>
/// </summary>
/// <remarks>TODO: this is a work in progress</remarks>
/// <typeparam name="TModel">The model type of the POCO</typeparam>
/// <typeparam name="TId">The id field type of the POCO and the entity</typeparam>
public class CachedRepository<TModel, TId> : IRepository<TModel, TId>
    where TModel : IEntity<TId>
    where TId : notnull
{
    private readonly ILogger _logger;
    private readonly IRepository<TModel, TId> _repository;

    private readonly IDictionary<TId, TModel> _items;
    //private static IDictionary<TId, TModel> Items = new Concurrent.ConcurrentDictionary<TId, TModel>();

    public CachedRepository(ILogger logger, IRepository<TModel, TId> repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        _items = new Dictionary<TId, TModel>();
    }

    public Task<IReadOnlyList<TModel>> GetAsync()
    {
        throw new NotImplementedException();

        // TODO: get from repository?

        // Get from cache
        //IReadOnlyList<TModel> items = _items.Values.ToList(); // TODO: return the source collection or a copy?
        //return Task.FromResult(items);
    }

    public async Task<TModel?> GetAsync(TId id)
    {
        // Get from cache
        if (_items.TryGetValue(id, out var value))
        {
            return value;
        }

        // Get from repository
        var item = await _repository.GetAsync(id);

        if (item is not null)
        {
            // Add to cache
            _items.TryAdd(item.Id, item);
        }

        return value;
    }

    public async Task<TModel> AddAsync(TModel model)
    {
        // Add to repository
        var item = await _repository.AddAsync(model);

        // Add to cache
        _items.TryAdd(item.Id, item);

        return item;
    }

    public Task<int?> UpdateAsync(TId id, TModel model)
    {
        // TODO: cache invalidation?

        // Update item in cache
        if (_items.ContainsKey(id))
        {
            _items[id] = model;
        }

        // Update item in repository
        return _repository.UpdateAsync(id, model);
    }

    public Task<int?> DeleteAsync(TId id)
    {
        // Delete item from cache
        _items.Remove(id); // TODO: return value if result if false?

        // Delete item from repository
        return _repository.DeleteAsync(id);
    }
}
