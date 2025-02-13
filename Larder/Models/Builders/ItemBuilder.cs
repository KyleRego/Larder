using Larder.Models.ItemComponents;

namespace Larder.Models.Builders;

public class ItemBuilder(string userId, string name,
                                string? description = null)
{
    private readonly string _userId = userId;
    private readonly string _name = name;
    private readonly string? _description = description;
    private string _id = Guid.NewGuid().ToString();
    private Quantity _quantity = Quantity.One();
    private readonly List<Action<Item>> _componentSetters = [];

    public ItemBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public ItemBuilder WithQuantity(Quantity quantity)
    {
        _quantity = quantity;
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

    public ItemBuilder WithNutrition(double calories, double gramsProtein)
    {
        _componentSetters.Add(item =>
        {
            item.Nutrition = new Nutrition
            {
                Item = item,
                ItemId = item.Id,
                Calories = calories,
                GramsProtein = gramsProtein
            };
        });

        return this;
    }

    public Item Build()
    {
        Item item = new(_userId, _name, _description)
        {
            Id = _id,
            Quantity = _quantity
        };

        foreach (var setter in _componentSetters)
        {
            setter(item);
        }

        return item;
    }
}
