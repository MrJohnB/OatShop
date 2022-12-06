using LiteBulb.OatShop.Domain.Dtos;
using LiteBulb.OatShop.Shared.Repositories;
using LiteBulb.OatShop.Shared.Services.Data;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Application.Services.Data;
public class OrderService : Service<Order>
{
    public OrderService(ILogger<OrderService> logger, IRepository<Order> orderRepository)
        : base(logger, orderRepository) { }
}
