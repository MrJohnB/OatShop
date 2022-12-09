using LiteBulb.OatShop.Shared.Entities;
using LiteBulb.OatShop.Shared.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Shared.Repositories.EntityFramework;
public abstract class AuditableRepository<TEntity, TModel> : Repository<TEntity, TModel>
    where TEntity : class, IEntity<int>, IAuditable
{
    public AuditableRepository(ILogger logger, DbContext dbContext, IMapper<TEntity, TModel> mapper)
        : base(logger, dbContext, mapper) { }

    public override async Task<TModel> AddAsync(TModel model)
    {
        var entity = Mapper.ToEntity(model);

        entity.Created = entity.LastModified = DateTimeOffset.UtcNow;

        DbContext.Add(entity);
        var entryCount = await DbContext.SaveChangesAsync();

        if (entryCount == 0)
        {
            throw new DbUpdateException();
        }

        // Return model with the database generated id
        return Mapper.ToModel(entity); // TODO: or just keep the model and get the generated id (model.Id = entity.Id)
    }

    public override async Task<int?> UpdateAsync(int id, TModel model)
    {
        // TODO: avoid 2 round trips to the database?

        var hasAny = await DbSet.AnyAsync(x => x.Id == id);

        if (!hasAny)
        {
            return null; // id not found
        }

        var entity = Mapper.ToEntity(model);

        entity.Id = id;

        entity.LastModified = DateTimeOffset.UtcNow;

        DbContext.Update(entity);
        DbContext.Entry(entity).Property(x => x.Created).IsModified = false; // don't update the Created timestamp
        return await DbContext.SaveChangesAsync(); // updated count
    }
}
