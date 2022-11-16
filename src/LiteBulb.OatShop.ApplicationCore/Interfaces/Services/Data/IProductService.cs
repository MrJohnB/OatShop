using LiteBulb.OatShop.ApplicationCore.Dtos;

namespace LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Data;
public interface IProductService
{
    Task<Product> AddAsync(Product dto);
    Task<int> DeleteAsync(int id);
    Task<ICollection<Product>> GetAsync();
    Task<Product?> GetAsync(int id);
    Task<int> UpdateAsync(Product dto);
}