namespace LiteBulb.OatShop.SharedKernel.Mappers;
public interface IMapper<TEntity, TModel>
{
    TModel ToModel(TEntity entity);
    ICollection<TModel> ToModel(ICollection<TEntity> entities);
    TEntity ToEntity(TModel model);
    ICollection<TEntity> ToEntity(ICollection<TModel> models);
}
