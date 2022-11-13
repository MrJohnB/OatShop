using LiteBulb.OatShop.ApplicationCore.Dtos;

namespace LiteBulb.OatShop.ApplicationCore.Interfaces.Services;
public interface ICustomerService
{
    Task<Customer> AddAsync(Customer dto);
    Task<int> DeleteAsync(int id);
    Task<ICollection<Customer>> GetAsync();
    Task<Customer?> GetAsync(int id);
    Task<int> UpdateAsync(Customer dto);
}
