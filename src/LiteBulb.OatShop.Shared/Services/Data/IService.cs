namespace LiteBulb.OatShop.Shared.Services.Data;
public interface IService<TModel, in TId>
{
    Task<ServiceResponse<IReadOnlyList<TModel>>> GetAsync();
    Task<ServiceResponse<TModel>> GetAsync(TId id);
    Task<ServiceResponse<TModel>> AddAsync(TModel model);
    Task<ServiceResponse<int>> UpdateAsync(TId id, TModel model);
    Task<ServiceResponse<int>> DeleteAsync(TId id);
}
