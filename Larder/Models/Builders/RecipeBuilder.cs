namespace Larder.Models.Builders;

public class RecipeBuilder(string userId, string name)
    : BuilderBase<RecipeBuilder>
{
    private readonly string _userId = userId;
    private readonly string _name = name;
    private readonly List<RecipeIngredient> _recipeIngredients = [];
    private readonly List<Item> _ingredients = [];

    public RecipeBuilder WithIngredient(Item ingredient, Quantity quantity)
    {
        RecipeIngredient recIng = new(_userId, _id, ingredient.Id)
        {
            DefaultQuantity = quantity
        };
        _recipeIngredients.Add(recIng);
        _ingredients.Add(ingredient);
        return this;
    }

    public RecipeBuilder WithIngredient(Item ingredient, double amount, Unit? unit)
    {
        Quantity quantity = new()
        {
            Amount = amount,
            Unit = unit,
            UnitId = unit?.Id
        };

        return WithIngredient(ingredient, quantity);
    }
    
    public RecipeBuilder WithIngredient(Item ingredient, double amount)
    {
        Quantity quantity = Quantity.Scalar(amount);

        return WithIngredient(ingredient, quantity);
    }

    public Recipe Build()
    {
        return new(_userId, _name)
        {
            Id = _id,
            RecipeIngredients = _recipeIngredients,
            Ingredients = _ingredients
        };
    }
}