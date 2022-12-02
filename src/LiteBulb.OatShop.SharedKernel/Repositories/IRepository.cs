namespace LiteBulb.OatShop.SharedKernel.Repositories;
public interface IRepository<T>
{
    Task<ICollection<T>> GetAsync();
    Task<T?> GetAsync(int id);
    Task<T> AddAsync(T dto);
    Task<int?> UpdateAsync(T dto);
    Task<int?> DeleteAsync(int id);
}
