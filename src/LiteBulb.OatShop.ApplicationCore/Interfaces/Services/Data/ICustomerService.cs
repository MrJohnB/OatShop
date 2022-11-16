using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Responses;

namespace LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Data;
public interface ICustomerService
{
    Task<ServiceResponse<Customer>> AddAsync(Customer customer);
    Task<ServiceResponse<int>> DeleteAsync(int id);
    Task<ServiceResponse<ICollection<Customer>>> GetAsync();
    Task<ServiceResponse<Customer>> GetAsync(int id);
    Task<ServiceResponse<int>> UpdateAsync(Customer customer);
}
