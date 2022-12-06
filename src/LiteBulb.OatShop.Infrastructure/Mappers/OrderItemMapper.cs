using LiteBulb.OatShop.Application.Dtos;
using LiteBulb.OatShop.SharedKernel.Mappers;

namespace LiteBulb.OatShop.Infrastructure.Mappers;
public class OrderItemMapper : IMapper<Entities.OrderItem, OrderItem>
{
    private readonly IMapper<Entities.Product, Product> _productMapper;

    public OrderItemMapper(IMapper<Entities.Product, Product> productMapper)
    {
        _productMapper = productMapper ?? throw new ArgumentNullException(nameof(productMapper));
    }

    public OrderItem ToModel(Entities.OrderItem entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        return new OrderItem()
        {
            Id = entity.Id,
            OrderId = entity.OrderId,
            Product = _productMapper.ToModel(entity.Product), // TODO: should OrderItem.ProductId (FK) be nullable?
            Name = entity.Name,
            OriginalPrice = entity.OriginalPrice,
            Discount = entity.Discount,
            Created = entity.Created,
            Updated = entity.Updated
        };
    }

    public IReadOnlyList<OrderItem> ToModel(IEnumerable<Entities.OrderItem> entities)
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        var models = new List<OrderItem>();

        foreach (var entity in entities)
        {
            models.Add(ToModel(entity));
        }

        return models;
    }

    public Entities.OrderItem ToEntity(OrderItem model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        return new Entities.OrderItem()
        {
            Id = model.Id,
            OrderId = model.OrderId,
            Product = _productMapper.ToEntity(model.Product), // TODO: should OrderItem.Product be nullable?
            Name = model.Name,
            OriginalPrice = model.OriginalPrice,
            Discount = model.Discount,
            Created = model.Created,
            Updated = model.Updated
        };
    }

    public IReadOnlyList<Entities.OrderItem> ToEntity(IEnumerable<OrderItem> models)
    {
        ArgumentNullException.ThrowIfNull(models, nameof(models));

        var entities = new List<Entities.OrderItem>();

        foreach (var model in models)
        {
            entities.Add(ToEntity(model));
        }

        return entities;
    }
}
