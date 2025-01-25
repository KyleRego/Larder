using Larder.Models;
using Larder.Models.ItemComponents;

namespace Larder.Dtos;

public class IngredientDto
{
    public string? Id { get; set; }

    public List<IngredientRecipeDto> Recipes { get; set; } = [];

    public static IngredientDto FromEntity(Ingredient ingredient)
    {
        IngredientDto dto = new()
        {
            Id = ingredient.Id
        };

        foreach (RecipeIngredient recIng in ingredient.RecipeIngredients)
        {
            Recipe? recipe = recIng.Recipe;
            ArgumentNullException.ThrowIfNull(recipe);

            IngredientRecipeDto recipeDto = new()
            {
                Id = recipe.Id,
                Name = recipe.Name
            };

            dto.Recipes.Add(recipeDto);
        }

        return dto;
    }

    // TODO: Remove this
    public static IngredientDto FromEntity(Item item)
    {
        ArgumentNullException.ThrowIfNull(item.Ingredient);

        return FromEntity(item.Ingredient);
    }
}

public class IngredientRecipeDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }
}
