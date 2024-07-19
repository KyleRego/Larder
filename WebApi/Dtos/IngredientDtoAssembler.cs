using Larder.Models;

namespace Larder.Dtos;

public static class IngredientDtoAssembler
{
    public static IngredientDto Assemble(Ingredient ingredient)
    {
        IngredientDto ingredientDto = new()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Quantity = ingredient.Quantity
        };

        if (ingredient.Unit != null)
        {
            ingredientDto.UnitName = ingredient.Unit.Name;
            ingredientDto.UnitId = ingredient.Unit.Id;
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

            ingredientDto.Recipes.Add(recipeDto);
        }

        return ingredientDto;
    }
}