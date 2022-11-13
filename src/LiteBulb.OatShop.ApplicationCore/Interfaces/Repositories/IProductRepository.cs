using LiteBulb.OatShop.ApplicationCore.Dtos;

namespace LiteBulb.OatShop.ApplicationCore.Interfaces.Repositories;
public interface IProductRepository
{
    Task<ICollection<Product>> GetAsync();
    Task<Product?> GetAsync(int id);
    Task<Product> AddAsync(Product dto);
    Task<int> UpdateAsync(Product dto);
    Task<int> DeleteAsync(int id);
}
