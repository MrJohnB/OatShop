﻿using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Repositories.Data;
using LiteBulb.OatShop.SharedKernel.Mappers;
using LiteBulb.OatShop.SharedKernel.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework;
public class OrderRepository : Repository<Entities.Order, Order>, IRepository<Order>
{
    public OrderRepository(ILogger<OrderRepository> logger, OatShopDbContext dbContext, IMapper<Entities.Order, Order> mapper)
        : base(logger, dbContext, mapper) { }

    public override async Task<ICollection<Order>> GetAsync()
    {
        var entities = await DbSet
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ToArrayAsync();

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
