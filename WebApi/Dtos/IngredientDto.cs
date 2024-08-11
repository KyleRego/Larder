using Larder.Models;

namespace Larder.Dtos;

public class IngredientDto : ItemDto
{
    public List<IngredientRecipeDto> Recipes { get; set; } = [];
}

public class IngredientRecipeDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }
}

public static class IngredientDtoAssembler
{
    public static IngredientDto Assemble(Ingredient ingredient)
    {
        IngredientDto dto = new()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Quantity = new()
            {
                Amount = ingredient.Quantity?.Amount ?? 0,
                UnitId = ingredient.Quantity?.UnitId,
                UnitName = ingredient.Quantity?.Unit?.Name
            }
        };

        foreach (RecipeIngredient recipeIngredient in ingredient.RecipeIngredients)
        {
            Recipe? recipe = recipeIngredient.Recipe;
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
}
