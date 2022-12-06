using LiteBulb.OatShop.Application.Dtos;
using LiteBulb.OatShop.Shared.Mappers;
using LiteBulb.OatShop.Shared.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework;
public class CustomerRepository : Repository<Entities.Customer, Customer>
{
    public CustomerRepository(ILogger<CustomerRepository> logger, OatShopDbContext dbContext, IMapper<Entities.Customer, Customer> mapper)
        : base(logger, dbContext, mapper) { }

    public override async Task<IReadOnlyList<Customer>> GetAsync()
    {
        var entities = await DbSet
            .Include(x => x.Orders)
            .ThenInclude(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ToListAsync();

        return Mapper.ToModel(entities);
    }

    public override async Task<Customer?> GetAsync(int id)
    {
        var entity = await DbSet
            .Include(x => x.Orders)
            .ThenInclude(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return null;
        }

        return Mapper.ToModel(entity);
    }
}
