namespace Larder.Models;

/// <summary>
/// An ingredient is used in cooking a food
/// </summary>
public class Ingredient : Item
{
    public required Quantity Quantity { get; set; } = new() { Amount = 0 };

    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    public List<Recipe> Recipes { get; set; } = [];
}
