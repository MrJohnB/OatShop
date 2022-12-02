namespace LiteBulb.OatShop.SharedKernel.Services.Data;
public interface IService<T>
{
    Task<ServiceResponse<ICollection<T>>> GetAsync();
    Task<ServiceResponse<T>> GetAsync(int id);
    Task<ServiceResponse<T>> AddAsync(T customer);
    Task<ServiceResponse<int>> UpdateAsync(T customer);
    Task<ServiceResponse<int>> DeleteAsync(int id);
}
