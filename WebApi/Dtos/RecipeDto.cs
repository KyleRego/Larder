using Larder.Models;

namespace Larder.Dtos;

public class RecipeDto
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required List<IngredientDto> Ingredients { get; set; }

    public static RecipeDto FromEntity(Recipe recipe)
    {
        RecipeDto recipeDto = new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Ingredients = []
        };

        foreach (Ingredient ingredient in recipe.Ingredients)
        {
            IngredientDto ingredientDto = new()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = QuantityDto.FromEntity(ingredient.Quantity)
            };

            recipeDto.Ingredients.Add(ingredientDto);
        }

        return recipeDto;
    }
}
