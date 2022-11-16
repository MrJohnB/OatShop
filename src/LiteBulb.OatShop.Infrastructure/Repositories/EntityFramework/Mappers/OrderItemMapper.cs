using LiteBulb.OatShop.ApplicationCore.Dtos;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Mappers;
internal static class OrderItemMapper
{
    internal static OrderItem Map(this Entities.OrderItem entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        return new OrderItem()
        {
            Id = entity.Id,
            OrderId = entity.OrderId,
            Product = entity.Product?.Map(),
            Name = entity.Name,
            OriginalPrice = entity.OriginalPrice,
            Discount = entity.Discount,
            Created = entity.Created,
            Updated = entity.Updated
        };
    }

    internal static ICollection<OrderItem> MapMany(this ICollection<Entities.OrderItem> entities)
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        var dtos = new List<OrderItem>();

        foreach (var entity in entities)
        {
            dtos.Add(Map(entity));
        }

        return dtos;
    }

    internal static Entities.OrderItem Map(this OrderItem dto)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        return new Entities.OrderItem()
        {
            Id = dto.Id,
            OrderId = dto.OrderId,
            Product = dto.Product?.Map(),
            Name = dto.Name,
            OriginalPrice = dto.OriginalPrice,
            Discount = dto.Discount,
            Created = dto.Created,
            Updated = dto.Updated
        };
    }

    internal static ICollection<Entities.OrderItem> MapMany(this ICollection<OrderItem> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos, nameof(dtos));

        var entities = new List<Entities.OrderItem>();

        foreach (var dto in dtos)
        {
            entities.Add(Map(dto));
        }

        return entities;
    }
}
