using LiteBulb.OatShop.Shared.Repositories;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Shared.Services.Data;
public abstract class Service<TModel, TId> : IService<TModel, TId>
{
    private readonly ILogger _logger;
    private readonly IRepository<TModel, TId> _repository;

    private readonly string _modelName = typeof(TModel).Name;

    public Service(ILogger logger, IRepository<TModel, TId> repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public virtual async Task<ServiceResponse<IReadOnlyList<TModel>>> GetAsync()
    {
        var result = await _repository.GetAsync();

        if (result is null)
        {
            return new ServiceResponse<IReadOnlyList<TModel>>(true,
                $"Error occurred while retrieving list of {_modelName} objects.  Result was null for some reason.");
        }

        return new ServiceResponse<IReadOnlyList<TModel>>(result);
    }

    public virtual async Task<ServiceResponse<TModel>> GetAsync(TId id)
    {
        var result = await _repository.GetAsync(id);

        if (result is null)
        {
            return new ServiceResponse<TModel>(true,
                $"Error occurred while retrieving {_modelName} object with id '{id}'.  {_modelName} object was not found in the database.");
        }

        return new ServiceResponse<TModel>(result);
    }

    public virtual async Task<ServiceResponse<TModel>> AddAsync(TModel model)
    {
        var result = await _repository.AddAsync(model);

        if (result is null)
        {
            return new ServiceResponse<TModel>(true,
                $"Error occurred while adding a {_modelName} object to database.  Result returned by add process was null for some reason.");
        }

        return new ServiceResponse<TModel>(result);
    }

    public virtual async Task<ServiceResponse<int>> UpdateAsync(TId id, TModel model)
    {
        var affectedCount = await _repository.UpdateAsync(id, model);

        if (affectedCount is null || !affectedCount.HasValue)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while updating a {_modelName} object in the database.  {_modelName} object with id '{id}' was not found in the database.");
        }

        if (affectedCount < 1)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while updating a {_modelName} object in the database.  The affected record count is '{affectedCount}' which is invalid.");
        }

        return new ServiceResponse<int>(affectedCount.Value);
    }

    public virtual async Task<ServiceResponse<int>> DeleteAsync(TId id)
    {
        var affectedCount = await _repository.DeleteAsync(id);

        if (affectedCount is null || !affectedCount.HasValue)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while deleting a {_modelName} object from the database.  {_modelName} object with id '{id}' was not found in the database.");
        }

        if (affectedCount < 1)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while deleting a {_modelName} object from the database.  The affected record count is '{affectedCount}' which is invalid.");
        }

        return new ServiceResponse<int>(affectedCount.Value);
    }
}
