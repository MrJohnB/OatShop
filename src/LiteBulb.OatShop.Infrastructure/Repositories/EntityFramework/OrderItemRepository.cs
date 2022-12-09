using LiteBulb.OatShop.Domain.Dtos;
using LiteBulb.OatShop.Infrastructure.Shared.Repositories.EntityFramework;
using LiteBulb.OatShop.Shared.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework;
public class OrderItemRepository : AuditableRepository<Entities.OrderItem, OrderItem, int>
{
    public OrderItemRepository(ILogger<OrderItemRepository> logger, OatShopDbContext dbContext, IMapper<Entities.OrderItem, OrderItem> mapper)
        : base(logger, dbContext, mapper) { }

    public override async Task<IReadOnlyList<OrderItem>> GetAsync()
    {
        var entities = await DbSet
            .Include(x => x.Product)
            .ToListAsync();

        return Mapper.ToModel(entities);
    }

    public override async Task<OrderItem?> GetAsync(int id)
    {
        var entity = await DbSet
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return null;
        }

        return Mapper.ToModel(entity);
    }

    // TODO: add Create that checks if Product exists first

    // TODO: add Update that checks if Product exists first

    // TODO: DTO should only allow Product Id field, not object JSON
}
