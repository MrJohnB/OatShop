﻿using LiteBulb.OatShop.Shared.Entities;
using LiteBulb.OatShop.Shared.Mappers;
using LiteBulb.OatShop.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Shared.Repositories.EntityFramework;
public abstract class Repository<TEntity, TModel, TId> : IRepository<TModel, TId>
    where TEntity : class, IEntity<TId>
{
    private readonly ILogger _logger;
    private readonly DbContext _dbContext;
    private readonly IMapper<TEntity, TModel> _mapper;

    protected ILogger Logger => _logger;
    protected DbContext DbContext => _dbContext;
    protected IMapper<TEntity, TModel> Mapper => _mapper;
    protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

    public Repository(ILogger logger, DbContext dbContext, IMapper<TEntity, TModel> mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public virtual async Task<IReadOnlyList<TModel>> GetAsync()
    {
        var entities = await DbSet.ToListAsync();

        return _mapper.ToModel(entities);
    }

    public virtual async Task<TModel?> GetAsync(TId id)
    {
        var entity = await DbSet.FindAsync(id);

        if (entity is default(TEntity))
        {
            return default;
        }

        return _mapper.ToModel(entity);
    }

    public virtual async Task<TModel> AddAsync(TModel model)
    {
        var entity = _mapper.ToEntity(model);

        DbContext.Add(entity);
        var entryCount = await DbContext.SaveChangesAsync();

        if (entryCount == 0)
        {
            throw new DbUpdateException();
        }

        // Return model with the database generated id
        return _mapper.ToModel(entity); // TODO: or just keep the model and get the generated id (model.Id = entity.Id)
    }

    public virtual async Task<int?> UpdateAsync(TId id, TModel model)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));

        // TODO: avoid 2 round trips to the database?

        var hasAny = await DbSet.AnyAsync(x => id.Equals(x.Id));

        if (!hasAny)
        {
            return null; // id not found
        }

        var entity = _mapper.ToEntity(model);

        entity.Id = id;

        DbContext.Update(entity);
        return await DbContext.SaveChangesAsync(); // updated count
    }

    public virtual async Task<int?> DeleteAsync(TId id)
    {
        // TODO: avoid 2 round trips to the database?

        var entity = await DbSet.FindAsync(id);

        if (entity is default(TEntity))
        {
            return null; // id not found
        }

        DbContext.Remove(entity);
        return await DbContext.SaveChangesAsync(); // deleted count
    }
}
