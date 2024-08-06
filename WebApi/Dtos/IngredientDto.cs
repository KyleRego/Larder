using Larder.Models;

namespace Larder.Dtos;

public class IngredientDto : ItemDto
{
    public string? Id { get; set; }

    public ItemQuantityDto? Quantity { get; set; }

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
            Name = ingredient.Name
        };

        if (ingredient.Quantity != null)
        {
            dto.Quantity = ItemQuantityDtoAssembler.Assemble(ingredient.Quantity);
        }

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
