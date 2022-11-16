using LiteBulb.OatShop.ApplicationCore.Dtos;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Mappers;
internal static class OrderMapper
{
    internal static Order Map(this Entities.Order entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        return new Order()
        {
            Id = entity.Id,
            CompanyId = entity.CompanyId,
            CustomerId = entity.CustomerId,
            OrderStatus = entity.OrderStatus,
            Discount = entity.Discount,
            OrderItems = entity.OrderItems.MapMany(),
            Created = entity.Created,
            Updated = entity.Updated
        };
    }

    internal static ICollection<Order> MapMany(this ICollection<Entities.Order> entities)
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        var dtos = new List<Order>();

        foreach (var entity in entities)
        {
            dtos.Add(Map(entity));
        }

        return dtos;
    }

    internal static Entities.Order Map(this Order dto)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        return new Entities.Order()
        {
            Id = dto.Id,
            CompanyId = dto.CompanyId,
            CustomerId = dto.CustomerId,
            OrderStatus = dto.OrderStatus,
            Discount = dto.Discount,
            OrderItems = dto.OrderItems.MapMany(),
            Created = dto.Created,
            Updated = dto.Updated
        };
    }

    internal static ICollection<Entities.Order> MapMany(this ICollection<Order> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos, nameof(dtos));

        var entities = new List<Entities.Order>();

        foreach (var dto in dtos)
        {
            entities.Add(Map(dto));
        }

        return entities;
    }
}
