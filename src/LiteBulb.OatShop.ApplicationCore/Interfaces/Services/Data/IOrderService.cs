using LiteBulb.OatShop.ApplicationCore.Dtos;

namespace LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Data;
public interface IOrderService
{
    Task<Order> AddAsync(Order dto);
    Task<int> DeleteAsync(int id);
    Task<ICollection<Order>> GetAsync();
    Task<Order?> GetAsync(int id);
    Task<int> UpdateAsync(Order dto);
}