using LiteBulb.OatShop.ApplicationCore.Dtos;

namespace LiteBulb.OatShop.ApplicationCore.Interfaces.Repositories.Data;
public interface IOrderRepository
{
    Task<ICollection<Order>> GetAsync();
    Task<Order?> GetAsync(int id);
    Task<Order> AddAsync(Order dto);
    Task<int> UpdateAsync(Order dto);
    Task<int> DeleteAsync(int id);
}
