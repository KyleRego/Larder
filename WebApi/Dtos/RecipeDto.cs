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

        foreach (RecipeIngredient recipeIngredient in recipe.RecipeIngredients)
        {
            IngredientDto ingredientDto = new()
            {
                Id = recipeIngredient.Id,
                Name = recipeIngredient.Ingredient.Name,
                Quantity = QuantityDto.FromEntity(recipeIngredient.Quantity)
            };

            recipeDto.Ingredients.Add(ingredientDto);
        }

        return recipeDto;
    }
}
