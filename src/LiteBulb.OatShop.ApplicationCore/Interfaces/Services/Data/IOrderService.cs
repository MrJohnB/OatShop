using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Responses;

namespace LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Data;
public interface IOrderService
{
    Task<ServiceResponse<Order>> AddAsync(Order order);
    Task<ServiceResponse<int>> DeleteAsync(int id);
    Task<ServiceResponse<ICollection<Order>>> GetAsync();
    Task<ServiceResponse<Order>> GetAsync(int id);
    Task<ServiceResponse<int>> UpdateAsync(Order order);
}
