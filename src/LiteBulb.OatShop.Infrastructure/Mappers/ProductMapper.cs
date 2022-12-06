using LiteBulb.OatShop.Application.Dtos;
using LiteBulb.OatShop.SharedKernel.Mappers;

namespace LiteBulb.OatShop.Infrastructure.Mappers;
public class ProductMapper : IMapper<Entities.Product, Product>
{
    public Product ToModel(Entities.Product entity)
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

    public IReadOnlyList<Product> ToModel(IEnumerable<Entities.Product> entities)
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        var models = new List<Product>();

        foreach (var entity in entities)
        {
            models.Add(ToModel(entity));
        }

        return models;
    }

    public Entities.Product ToEntity(Product model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        return new Entities.Product()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Category = model.Category,
            OriginalPrice = model.OriginalPrice,
            Discount = model.Discount,
            Created = model.Created,
            Updated = model.Updated
        };
    }

    public IReadOnlyList<Entities.Product> ToEntity(IEnumerable<Product> models)
    {
        ArgumentNullException.ThrowIfNull(models, nameof(models));

        var entities = new List<Entities.Product>();

        foreach (var model in models)
        {
            entities.Add(ToEntity(model));
        }

        return entities;
    }
}
