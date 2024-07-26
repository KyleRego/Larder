namespace Larder.Services.Tests;

using Moq;

using Larder.Models;
using Larder.Services;
using Larder.Repository;
using Larder.Dtos;

public class RecipeServiceTests
{
    [Fact]
    public async void UpdateRecipeThrowsIfIdIsNull()
    {
        var recipeRepository = new Mock<IRecipeRepository>();
        var ingredientRepository = new Mock<IIngredientRepository>();

        RecipeService service = new(recipeRepository.Object, ingredientRepository.Object);

        RecipeDto recipe = new()
        {
            Name = "New recipe",
            Ingredients = [
                new() {
                    IngredientName = "Eggs"
                }
            ]
        };

        await Assert.ThrowsAsync<ApplicationException>(async () => await service.UpdateRecipe(recipe));
    }

    [Fact]
    public async void UpdateRecipeThrowsIfRecipeNotFound()
    {
        var recipeRepository = new Mock<IRecipeRepository>();
        var ingredientRepository = new Mock<IIngredientRepository>();

        string id = "made_up_id";
        recipeRepository.Setup(r => r.Get(id)).ReturnsAsync((Recipe?)null);

        RecipeService service = new(recipeRepository.Object, ingredientRepository.Object);

        RecipeDto recipe = new()
        {
            Id = id,
            Name = "New recipe",
            Ingredients = [
                new() {
                    IngredientName = "Eggs"
                }
            ]
        };

        await Assert.ThrowsAsync<ApplicationException>(async () => await service.UpdateRecipe(recipe));
    }
}