namespace Larder.Models.ItemComponents;

public class Ingredient : ItemComponent
{
    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];

    public List<Recipe> Recipes { get; set; } = [];
}
