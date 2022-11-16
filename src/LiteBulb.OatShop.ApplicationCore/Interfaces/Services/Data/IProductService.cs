using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Responses;

namespace LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Data;
public interface IProductService
{
    Task<ServiceResponse<Product>> AddAsync(Product product);
    Task<ServiceResponse<int>> DeleteAsync(int id);
    Task<ServiceResponse<ICollection<Product>>> GetAsync();
    Task<ServiceResponse<Product>> GetAsync(int id);
    Task<ServiceResponse<int>> UpdateAsync(Product product);
}
