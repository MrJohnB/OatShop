namespace LiteBulb.OatShop.Shared.Repositories;
public interface IRepository<TModel, in TId>
{
    Task<IReadOnlyList<TModel>> GetAsync();
    Task<TModel?> GetAsync(TId id);
    Task<TModel> AddAsync(TModel model);
    Task<int?> UpdateAsync(TId id, TModel model);
    Task<int?> DeleteAsync(TId id);
}
