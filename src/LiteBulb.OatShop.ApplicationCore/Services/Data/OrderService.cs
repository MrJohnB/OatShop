using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Responses;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Repositories.Data;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Services.Data;
using Microsoft.Extensions.Logging;


namespace LiteBulb.OatShop.ApplicationCore.Services.Data;
public class OrderService : IOrderService
{
    private readonly ILogger<OrderService> _logger;
    private readonly IRepository<Order> _orderRepository;

    public OrderService(ILogger<OrderService> logger, IRepository<Order> orderRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<ServiceResponse<ICollection<Order>>> GetAsync()
    {
        var result = await _orderRepository.GetAsync();

        if (result is null)
        {
            return new ServiceResponse<ICollection<Order>>(true,
                "Error occurred while retrieving list of Order objects.  Result was null for some reason.");
        }

        return new ServiceResponse<ICollection<Order>>(result);
    }

    public async Task<ServiceResponse<Order>> GetAsync(int id)
    {
        var result = await _orderRepository.GetAsync(id);

        if (result is null)
        {
            return new ServiceResponse<Order>(true,
                $"Error occurred while retrieving Order object with id '{id}'.  Order object was not found in the database.");
        }

        return new ServiceResponse<Order>(result);
    }

    public async Task<ServiceResponse<Order>> AddAsync(Order order)
    {
        var result = await _orderRepository.AddAsync(order);

        if (result is null)
        {
            return new ServiceResponse<Order>(true,
                "Error occurred while adding a Order object to database.  Result returned by add process was null for some reason.");
        }

        if (result.Id < 1)
        {
            return new ServiceResponse<Order>(true,
                $"Error occurred while adding a Order object to database.  Result returned by add process has an id of {result.Id} which is invalid.");
        }

        return new ServiceResponse<Order>(result);
    }

    public async Task<ServiceResponse<int>> UpdateAsync(Order order)
    {
        var affectedCount = await _orderRepository.UpdateAsync(order);

        if (affectedCount is null || !affectedCount.HasValue)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while updating a Order object in the database.  Order object with id '{order.Id}' was not found in the database.");
        }

        if (affectedCount < 1)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while updating a Order object in the database.  The affected record count is '{affectedCount}' which is invalid.");
        }

        return new ServiceResponse<int>(affectedCount.Value);
    }

    public async Task<ServiceResponse<int>> DeleteAsync(int id)
    {
        var affectedCount = await _orderRepository.DeleteAsync(id);

        if (affectedCount is null || !affectedCount.HasValue)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while deleting a Order object from the database.  Order object with id '{id}' was not found in the database.");
        }

        if (affectedCount < 1)
        {
            return new ServiceResponse<int>(true,
               $"Error occurred while deleting a Order object from the database.  The affected record count is '{affectedCount}' which is invalid.");
        }

        return new ServiceResponse<int>(affectedCount.Value);
    }
}
