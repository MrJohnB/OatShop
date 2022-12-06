namespace LiteBulb.OatShop.Shared.Services.Data;
public interface IService<T>
{
    Task<ServiceResponse<IReadOnlyList<T>>> GetAsync();
    Task<ServiceResponse<T>> GetAsync(int id);
    Task<ServiceResponse<T>> AddAsync(T customer);
    Task<ServiceResponse<int>> UpdateAsync(T customer);
    Task<ServiceResponse<int>> DeleteAsync(int id);
}
