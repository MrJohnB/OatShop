namespace LiteBulb.OatShop.Shared.Repositories;
public interface IRepository<T>
{
    Task<IReadOnlyList<T>> GetAsync();
    Task<T?> GetAsync(int id);
    Task<T> AddAsync(T model);
    Task<int?> UpdateAsync(T model);
    Task<int?> DeleteAsync(int id);
}
