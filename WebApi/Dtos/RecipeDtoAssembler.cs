using Larder.Models;

namespace Larder.Dtos;

public static class RecipeDtoAssembler
{
    public static RecipeDto Assemble(Recipe recipe)
    {
        RecipeDto recipeDto = new()
        {
            RecipeId = recipe.Id,
            RecipeName = recipe.Name,
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