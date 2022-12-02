using LiteBulb.OatShop.ApplicationCore.Dtos;
using LiteBulb.OatShop.SharedKernel.Mappers;

namespace LiteBulb.OatShop.Infrastructure.Mappers;
public class OrderMapper : IMapper<Entities.Order, Order>
{
    private readonly IMapper<Entities.OrderItem, OrderItem> _orderItemMapper;

    public OrderMapper(IMapper<Entities.OrderItem, OrderItem> orderItemMapper)
    {
        _orderItemMapper = orderItemMapper ?? throw new ArgumentNullException(nameof(orderItemMapper));
    }

    public Order ToModel(Entities.Order entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        return new Order()
        {
            Id = entity.Id,
            CompanyId = entity.CompanyId,
            CustomerId = entity.CustomerId,
            OrderStatus = entity.OrderStatus,
            Discount = entity.Discount,
            OrderItems = _orderItemMapper.ToModel(entity.OrderItems),
            Created = entity.Created,
            Updated = entity.Updated
        };
    }

    public ICollection<Order> ToModel(ICollection<Entities.Order> entities)
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        var dtos = new List<Order>();

        foreach (var entity in entities)
        {
            dtos.Add(ToModel(entity));
        }

        return dtos;
    }

    public Entities.Order ToEntity(Order model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        return new Entities.Order()
        {
            Id = model.Id,
            CompanyId = model.CompanyId,
            CustomerId = model.CustomerId,
            OrderStatus = model.OrderStatus,
            Discount = model.Discount,
            OrderItems = _orderItemMapper.ToEntity(model.OrderItems),
            Created = model.Created,
            Updated = model.Updated
        };
    }

    public ICollection<Entities.Order> ToEntity(ICollection<Order> models)
    {
        ArgumentNullException.ThrowIfNull(models, nameof(models));

        var entities = new List<Entities.Order>();

        foreach (var model in models)
        {
            entities.Add(ToEntity(model));
        }

        return entities;
    }
}
