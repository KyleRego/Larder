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

    public ItemBuilder WithNutrition(NutritionBuilder nutritionBuilder)
    {
        _nutritionBuilder = nutritionBuilder;
        return this;
    }

    public Item Build()
    {
        Item item = new(_userId, _name, _description)
        {
            Id = _id,
            Quantity = _quantity
        };

        if (_nutritionBuilder is not null)
        {
            item.Nutrition = _nutritionBuilder.Build(item);
        }

        return item;
    }
}
