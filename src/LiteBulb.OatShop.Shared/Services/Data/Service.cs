using LiteBulb.OatShop.Shared.Exceptions;
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

    private bool IsDefaultValue(TId value) => EqualityComparer<TId>.Default.Equals(value, default);

    public virtual async Task<ServiceResponse<IReadOnlyList<TModel>>> GetAsync()
    {
        var result = await _repository.GetAsync();

        if (result is null)
        {
            return new ServiceResponse<IReadOnlyList<TModel>>(true,
                $"Error occurred while retrieving list of {_modelName} objects.  Result was null for some reason.",
                new InternalErrorException());
        }

        if (result.Count == 0)
        {
            return new ServiceResponse<IReadOnlyList<TModel>>(true,
                "Empty list.",
                new NotFoundException());
        }

        return new ServiceResponse<IReadOnlyList<TModel>>(result);
    }

    public virtual async Task<ServiceResponse<TModel>> GetAsync(TId id)
    {
        if (id is null)
        {
            return new ServiceResponse<TModel>(true,
                $"Id parameter cannot be null for Find By Id.",
                new BadRequestException());
        }

        if (IsDefaultValue(id))
        {
            return new ServiceResponse<TModel>(true,
                $"Id parameter cannot contain default value: '{id}' for Find By Id.",
                new BadRequestException());
        }

        var result = await _repository.GetAsync(id);

        if (result is null)
        {
            return new ServiceResponse<TModel>(true,
                $"{_modelName} object with id '{id}' was not found.",
                new NotFoundException());
        }

        return new ServiceResponse<TModel>(result);
    }

    public virtual async Task<ServiceResponse<TModel>> AddAsync(TModel model)
    {
        if (model is null)
        {
            return new ServiceResponse<TModel>(true,
                $"{_modelName} object payload cannot be null for Create.",
                new BadRequestException());
        }

        var result = await _repository.AddAsync(model);

        if (result is null)
        {
            return new ServiceResponse<TModel>(true,
                $"Error occurred while adding a {_modelName} object to database.  Result returned by add process was null for some reason.",
                new InternalErrorException());
        }

        return new ServiceResponse<TModel>(result);
    }

    public virtual async Task<ServiceResponse<int>> UpdateAsync(TId id, TModel model)
    {
        if (id is null)
        {
            return new ServiceResponse<int>(true,
                $"Id parameter cannot be null for Update.",
                new BadRequestException());
        }

        if (IsDefaultValue(id))
        {
            return new ServiceResponse<int>(true,
                $"Id parameter cannot contain default value: '{id}' for Update.",
                new BadRequestException());
        }

        if (model is null)
        {
            return new ServiceResponse<int>(true,
                $"{_modelName} object payload cannot be null for Update.",
                new BadRequestException());
        }

        var affectedCount = await _repository.UpdateAsync(id, model);

        if (affectedCount is null || !affectedCount.HasValue)
        {
            return new ServiceResponse<int>(true,
               $"{_modelName} object with id '{id}' was not found.",
               new NotFoundException());
        }

        if (affectedCount < 1)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while updating a {_modelName} object in the database.  The affected record count is '{affectedCount}' which is invalid.",
               new InternalErrorException());
        }

        return new ServiceResponse<int>(affectedCount.Value);
    }

    public virtual async Task<ServiceResponse<int>> DeleteAsync(TId id)
    {
        if (id is null)
        {
            return new ServiceResponse<int>(true,
                $"Id parameter cannot be null for Delete By Id.",
                new BadRequestException());
        }

        if (IsDefaultValue(id))
        {
            return new ServiceResponse<int>(true,
                $"Id parameter cannot contain default value: '{id}' for Delete By Id.",
                new BadRequestException());
        }

        var affectedCount = await _repository.DeleteAsync(id);

        if (affectedCount is null || !affectedCount.HasValue)
        {
            return new ServiceResponse<int>(true,
               $"{_modelName} object with id '{id}' was not found.",
               new NotFoundException());
        }

        if (affectedCount < 1)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while deleting a {_modelName} object from the database.  The affected record count is '{affectedCount}' which is invalid.",
               new InternalErrorException());
        }

        return new ServiceResponse<int>(affectedCount.Value);
    }
}
