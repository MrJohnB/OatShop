using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Repositories;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.ApplicationCore.Services;
public class OrderService : IOrderService
{
    private readonly ILogger<OrderService> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderService(ILogger<OrderService> logger, IOrderRepository orderRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<ICollection<Order>> GetAsync()
    {
        return await _orderRepository.GetAsync();
    }

    public async Task<Order?> GetAsync(int id)
    {
        return await _orderRepository.GetAsync(id);
    }

    public async Task<Order> AddAsync(Order dto)
    {
        return await _orderRepository.AddAsync(dto);
    }

    public async Task<int> UpdateAsync(Order dto)
    {
        return await _orderRepository.UpdateAsync(dto);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _orderRepository.DeleteAsync(id);
    }
}
