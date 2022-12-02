using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.SharedKernel.Mappers;
using LiteBulb.OatShop.SharedKernel.Repositories;
using LiteBulb.OatShop.SharedKernel.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework;
public class OrderItemRepository : Repository<Entities.OrderItem, OrderItem>, IRepository<OrderItem>
{
    public OrderItemRepository(ILogger<OrderItemRepository> logger, OatShopDbContext dbContext, IMapper<Entities.OrderItem, OrderItem> mapper)
        : base(logger, dbContext, mapper) { }

    public override async Task<ICollection<OrderItem>> GetAsync()
    {
        var entities = await DbSet
            .Include(x => x.Product)
            .ToArrayAsync();

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
}
