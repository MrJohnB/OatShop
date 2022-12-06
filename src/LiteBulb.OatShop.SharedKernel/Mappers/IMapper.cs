namespace LiteBulb.OatShop.SharedKernel.Mappers;
public interface IMapper<TEntity, TModel>
{
    TModel ToModel(TEntity entity);
    IReadOnlyList<TModel> ToModel(IEnumerable<TEntity> entities);
    TEntity ToEntity(TModel model);
    IReadOnlyList<TEntity> ToEntity(IEnumerable<TModel> models);
}
