using Larder.Models;

namespace Larder.Dtos;

public class RecipeIngredientDto
{
    public string? Id { get; set; }
    public required string Name { get; set; }

    public required QuantityDto Quantity { get; set; }
}

public class RecipeDto
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required List<RecipeIngredientDto> Ingredients { get; set; }

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
            RecipeIngredientDto riDto = new()
            {
                Id = recipeIngredient.Id,
                Name = recipeIngredient.Ingredient.Item.Name,
                Quantity = QuantityDto.FromEntity(recipeIngredient.Quantity)
            };

            recipeDto.Ingredients.Add(riDto);
        }

        return recipeDto;
    }
}