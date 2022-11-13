using LiteBulb.OatShop.ApplicationCore.Dtos;

namespace LiteBulb.OatShop.ApplicationCore.Interfaces.Repositories;
public interface ICustomerRepository
{
    Task<ICollection<Customer>> GetAsync();
    Task<Customer?> GetAsync(int id);
    Task<Customer> AddAsync(Customer dto);
    Task<int> UpdateAsync(Customer dto);
    Task<int> DeleteAsync(int id);
}
