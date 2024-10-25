using Larder.Models;

namespace Larder.Dtos;

public class IngredientDto
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required QuantityDto Quantity { get; set; }

    public List<IngredientRecipeDto> Recipes { get; set; } = [];

    public static IngredientDto FromEntity(Item item)
    {
        ArgumentNullException.ThrowIfNull(item.Ingredient);

        IngredientDto dto = new()
        {
            Id = item.Id,
            Name = item.Name,
            Quantity = new()
            {
                Amount = item.Ingredient.Quantity.Amount,
                UnitId = item.Ingredient.Quantity?.UnitId,
                UnitName = item.Ingredient.Quantity?.Unit?.Name
            }
        };

        foreach (RecipeIngredient recIng in item.Ingredient.RecipeIngredients)
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
}

public class IngredientRecipeDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }
}
