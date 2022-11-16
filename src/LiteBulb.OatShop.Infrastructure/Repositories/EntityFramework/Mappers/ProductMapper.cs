using LiteBulb.OatShop.ApplicationCore.Dtos;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Mappers;
internal static class ProductMapper
{
    internal static Product Map(this Entities.Product entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        return new Product()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Category = entity.Category,
            OriginalPrice = entity.OriginalPrice,
            Discount = entity.Discount,
            Created = entity.Created,
            Updated = entity.Updated
        };
    }

    internal static ICollection<Product> MapMany(this ICollection<Entities.Product> entities)
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        var dtos = new List<Product>();
        
        foreach (var entity in entities)
        {
            dtos.Add(Map(entity));
        }

        return dtos;
    }

    internal static Entities.Product Map(this Product dto)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        return new Entities.Product()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Category = dto.Category,
            OriginalPrice = dto.OriginalPrice,
            Discount = dto.Discount,
            Created = dto.Created,
            Updated = dto.Updated
        };
    }

    internal static ICollection<Entities.Product> MapMany(this ICollection<Product> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos, nameof(dtos));

        var entities = new List<Entities.Product>();

        foreach (var dto in dtos)
        {
            entities.Add(Map(dto));
        }

        return entities;
    }
}
