using LiteBulb.OatShop.ApplicationCore.Dtos;

namespace LiteBulb.OatShop.ApplicationCore.Interfaces.Repositories;
public interface IOrderItemRepository
{
    Task<ICollection<OrderItem>> GetAsync();
    Task<OrderItem?> GetAsync(int id);
    Task<OrderItem> AddAsync(OrderItem dto);
    Task<int> UpdateAsync(OrderItem dto);
    Task<int> DeleteAsync(int id);
}
