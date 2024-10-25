namespace Larder.Models;

public class Ingredient : ItemComponent
{
    public required Quantity Quantity { get; set; } = new() { Amount = 0 };

    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    public List<Recipe> Recipes { get; set; } = [];
}
