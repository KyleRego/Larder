using Larder.Dtos;
using Larder.Models.ItemComponents;

namespace Larder.Models.Builders;

public class ItemBuilder(string userId, string name,
                                string? description = null)
    : BuilderBase<ItemBuilder>
{
    private readonly string _userId = userId;
    private readonly string _name = name;
    private readonly string? _description = description;
    private Quantity _quantity = Quantity.One();
    private NutritionBuilder? _nutritionBuilder;
    private ConsumedTimeBuilder? _consumedTimeBuilder;
    private List<Item>? _containedItems;

    public ItemBuilder(string userId, string name) : this(userId, name, null)
    {
        
    }

    public ItemBuilder WithQuantity(Quantity quantity)
    {
        _quantity = quantity;
        return this;
    }

    public ItemBuilder WithQuantity(QuantityDto quantity)
    {
        _quantity = new()
        {
            Amount = quantity.Amount,
            UnitId = quantity.UnitId
        };
        return this;
    }

    public ItemBuilder WithQuantity(double amount, Unit? unit = null)
    {
        _quantity = new()
        {
            Amount = amount,
            UnitId = unit?.Id
        };
        return this;
    }

    public ItemBuilder WithNutrition(NutritionBuilder nutritionBuilder)
    {
        _nutritionBuilder = nutritionBuilder;
        return this;
    }

    public ItemBuilder WithConsumedTime(ConsumedTimeBuilder consumedTimeBuilder)
    {
        _consumedTimeBuilder = consumedTimeBuilder;
        return this;
    }

    public ItemBuilder WithContainer()
    {
        _containedItems = [];
        return this;
    }

    public ItemBuilder WithContainedItems(List<Item> items)
    {
        _containedItems = items;
        return this;
    }

    public Item Build()
    {
        Item item = new(_userId, _name, _description)
        {
            Id = _id,
            Quantity = _quantity
        };

        if (_nutritionBuilder != null)
            item.Nutrition = _nutritionBuilder.Build(item);

        if (_consumedTimeBuilder != null)
            item.ConsumedTime = _consumedTimeBuilder.Build(item);

        if (_containedItems != null)
            item.Container = new Container()
            {
                Item = item,
                Items = _containedItems
            };

        return item;
    }

    // Maybe this shouldn't exist...
    public ItemDto BuildDto()
    {
        ItemDto dto = new()
        {
            Id = _id,
            Name = _name,
            Description = _description,
            Quantity = (QuantityDto)_quantity
        };

        if (_nutritionBuilder != null)
            dto.Nutrition = _nutritionBuilder.BuildDto();

        return dto;
    }
}
