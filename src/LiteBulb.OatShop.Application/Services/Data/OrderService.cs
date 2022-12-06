using LiteBulb.OatShop.Application.Dtos;
using LiteBulb.OatShop.SharedKernel.Repositories;
using LiteBulb.OatShop.SharedKernel.Services.Data;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Application.Services.Data;
public class OrderService : Service<Order>
{
    public OrderService(ILogger<OrderService> logger, IRepository<Order> orderRepository)
        : base(logger, orderRepository) { }
}
