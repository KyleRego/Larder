using Larder.Models;

namespace Larder.Dtos;

public class RecipeDto
{
    public string? Id { get; set; }

    public required string Name { get; set; }

    public required List<RecipeIngredientDto> Ingredients { get; set; }
}

public class RecipeIngredientDto
{
    public string? RecipeIngredientId { get; set; }

    public required string IngredientName { get; set; }

    public string? IngredientId { get; set; }

    public double Amount { get; set; }

    public string? UnitName { get; set; }

    public string? UnitId { get; set; }
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
            ArgumentNullException.ThrowIfNull(recipeIngredient.Ingredient);

            RecipeIngredientDto recipeDtoIngredient = new()
            {
                RecipeIngredientId = recipeIngredient.Id,

                IngredientName = recipeIngredient.Ingredient.Name,
                IngredientId = recipeIngredient.Ingredient.Id,

                Amount = recipeIngredient.Amount,

                UnitName = recipeIngredient.Unit?.Name,
                UnitId = recipeIngredient.UnitId
            };

            recipeDto.Ingredients.Add(recipeDtoIngredient);
        }

        return recipeDto;
    }
}
