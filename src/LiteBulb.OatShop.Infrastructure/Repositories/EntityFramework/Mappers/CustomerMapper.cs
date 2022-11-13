using LiteBulb.OatShop.ApplicationCore.Dtos;

namespace LiteBulb.OatShop.Infrastructure.Repositories.EntityFramework.Mappers;
internal static class CustomerMapper
{
    internal static Customer ToDto(this Entities.Customer entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        return new Customer()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            MobilePhone = entity.MobilePhone,
            Email = entity.Email,
            Line1 = entity.Line1,
            Line2 = entity.Line2,
            Line3 = entity.Line3,
            City = entity.City,
            ZipCode = entity.ZipCode,
            State = entity.State,
            County = entity.County,
            Country = entity.Country,
            Orders = entity.Orders.ToDto(),
            Created = entity.Created,
            Updated = entity.Updated
        };
    }

    internal static ICollection<Customer> ToDto(this ICollection<Entities.Customer> entities)
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        var dtos = new List<Customer>();

        foreach (var entity in entities)
        {
            dtos.Add(ToDto(entity));
        }

        return dtos;
    }

    internal static Entities.Customer ToEntity(this Customer dto)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        return new Entities.Customer()
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MobilePhone = dto.MobilePhone,
            Email = dto.Email,
            Line1 = dto.Line1,
            Line2 = dto.Line2,
            Line3 = dto.Line3,
            City = dto.City,
            ZipCode = dto.ZipCode,
            State = dto.State,
            County = dto.County,
            Country = dto.Country,
            Orders = dto.Orders.ToEntity(),
            Created = dto.Created,
            Updated = dto.Updated
        };
    }

    internal static ICollection<Entities.Customer> ToEntity(this ICollection<Customer> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos, nameof(dtos));

        var entities = new List<Entities.Customer>();

        foreach (var dto in dtos)
        {
            entities.Add(ToEntity(dto));
        }

        return entities;
    }
}
