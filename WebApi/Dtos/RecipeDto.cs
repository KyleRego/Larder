using Larder.Models;

namespace Larder.Dtos;

public class RecipeDto : DtoBase
{
    public required string Name { get; set; }

    public required List<RecipeIngredientDto> Ingredients { get; set; }
}

public class RecipeIngredientDto : DtoBase
{
    public required string IngredientName { get; set; }

    public string? IngredientId { get; set; }

    public double Amount { get; set; }

    public string? UnitId { get; set; }

    public string? UnitName { get; set; }
}

public static class RecipeDtoAssembler
{
    public static RecipeDto Assemble(Recipe recipe)
    {
        RecipeDto recipeDto = new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Ingredients = []
        };

        foreach (RecipeIngredient recipeIngredient in recipe.RecipeIngredients)
        {
            RecipeIngredientDto recipeDtoIngredient = new()
            {
                Id = recipeIngredient.Id,
                IngredientName = recipeIngredient.Ingredient.Name,
                IngredientId = recipeIngredient.Ingredient.Id,
                Amount = recipeIngredient.Quantity?.Amount ?? 0,
                UnitId = recipeIngredient.Quantity?.UnitId,
                UnitName = recipeIngredient.Quantity?.Unit?.Name
            };

            recipeDto.Ingredients.Add(recipeDtoIngredient);
        }

        return recipeDto;
    }
}
