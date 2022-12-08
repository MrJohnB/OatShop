using LiteBulb.OatShop.Domain.Dtos;
using LiteBulb.OatShop.Infrastructure.Shared.Repositories.EntityFramework;
using LiteBulb.OatShop.Shared.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework;
public class OrderRepository : AuditableRepository<Entities.Order, Order>
{
    public OrderRepository(ILogger<OrderRepository> logger, OatShopDbContext dbContext, IMapper<Entities.Order, Order> mapper)
        : base(logger, dbContext, mapper) { }

    public override async Task<IReadOnlyList<Order>> GetAsync()
    {
        var entities = await DbSet
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ToListAsync();

        return Mapper.ToModel(entities);
    }

    public override async Task<Order?> GetAsync(int id)
    {
        var entity = await DbSet
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return null;
        }

        return Mapper.ToModel(entity);
    }
}
