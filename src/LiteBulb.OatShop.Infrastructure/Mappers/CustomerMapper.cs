using LiteBulb.OatShop.Application.Dtos;
using LiteBulb.OatShop.Shared.Mappers;

namespace LiteBulb.OatShop.Infrastructure.Mappers;
public class CustomerMapper : IMapper<Entities.Customer, Customer>
{
    private readonly IMapper<Entities.Order, Order> _orderMapper;

    public CustomerMapper(IMapper<Entities.Order, Order> orderMapper)
    {
        _orderMapper = orderMapper ?? throw new ArgumentNullException(nameof(orderMapper));
    }

    public Customer ToModel(Entities.Customer entity)
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
            Orders = _orderMapper.ToModel(entity.Orders),
            Created = entity.Created,
            Updated = entity.Updated
        };
    }

    public IReadOnlyList<Customer> ToModel(IEnumerable<Entities.Customer> entities)
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        var models = new List<Customer>();

        foreach (var entity in entities)
        {
            models.Add(ToModel(entity));
        }

        return models;
    }

    public Entities.Customer ToEntity(Customer model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        return new Entities.Customer()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            MobilePhone = model.MobilePhone,
            Email = model.Email,
            Line1 = model.Line1,
            Line2 = model.Line2,
            Line3 = model.Line3,
            City = model.City,
            ZipCode = model.ZipCode,
            State = model.State,
            County = model.County,
            Country = model.Country,
            Orders = _orderMapper.ToEntity(model.Orders),
            Created = model.Created,
            Updated = model.Updated
        };
    }

    public IReadOnlyList<Entities.Customer> ToEntity(IEnumerable<Customer> models)
    {
        ArgumentNullException.ThrowIfNull(models, nameof(models));

        var entities = new List<Entities.Customer>();

        foreach (var model in models)
        {
            entities.Add(ToEntity(model));
        }

        return entities;
    }
}
