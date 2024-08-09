using Larder.Models;

namespace Larder.Dtos;

public class IngredientDto : ItemDto, IQuantityDto
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
            Amount = ingredient.Amount,
            UnitId = ingredient.UnitId,
            UnitName = ingredient.Unit?.Name
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
