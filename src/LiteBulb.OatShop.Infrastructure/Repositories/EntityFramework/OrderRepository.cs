﻿using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.ApplicationCore.Interfaces.Repositories.Data;
using LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework;
public class OrderRepository : IOrderRepository
{
    private readonly ILogger<OrderRepository> _logger;
    private readonly OatShopDbContext _dbContext;

    private DbSet<Entities.Order> _dbSet => _dbContext.Set<Entities.Order>();

    public OrderRepository(ILogger<OrderRepository> logger, OatShopDbContext dbContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<ICollection<Order>> GetAsync()
    {
        var entities = await _dbSet
            .Include(x => x.OrderItems)
            .ToArrayAsync();

        return entities.MapMany();
    }

    public async Task<Order?> GetAsync(int id)
    {
        var entity = await _dbSet.Include(x => x.OrderItems)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return null;
        }

        return entity.Map();
    }

    public async Task<Order> AddAsync(Order dto)
    {
        var entity = dto.Map();

        _dbContext.Add(entity);
        var entryCount = await _dbContext.SaveChangesAsync();

        if (entryCount == 0)
        {
            throw new DbUpdateException();
        }

        // Get the id generated by database
        dto.Id = entity.Id;
        return dto;
    }

    public async Task<int?> UpdateAsync(Order dto)
    {
        // TODO: 2 round trips to the database

        var hasAny = await _dbSet
            .AnyAsync(x => x.Id == dto.Id);

        if (!hasAny)
        {
            return null; // id not found
        }

        var entity = dto.Map();

        _dbContext.Update(entity);
        return await _dbContext.SaveChangesAsync(); // updated count
    }

    public async Task<int?> DeleteAsync(int id)
    {
        // TODO: 2 round trips to the database

        var entity = await _dbSet.SingleOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return null; // id not found
        }

        _dbContext.Remove(entity);
        return await _dbContext.SaveChangesAsync(); // deleted count
    }
}
