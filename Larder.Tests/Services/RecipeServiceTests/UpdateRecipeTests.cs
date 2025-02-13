using Larder.Models;
using Larder.Dtos;
using Larder.Services.Interface;
using Larder.Services.Impl;
using Larder.Repository.Interface;

namespace Larder.Tests.Services.RecipeServiceTests;

public class RecipeServiceTests : ServiceTestsBase
{
    [Fact]
    public async void UpdateRecipeThrowsIfIdIsNull()
    {
        var recipeRepository = new Mock<IRecipeRepository>();
        var ingredientRepository = new Mock<IIngredientRepository>();
        var foodRepository = new Mock<IFoodRepository>();
        var quantityMathService = new Mock<IQuantityService>();

        RecipeService sut = new(_serviceProvider.Object, recipeRepository.Object,
                                    ingredientRepository.Object,
                                    foodRepository.Object,
                                    quantityMathService.Object);

        RecipeDto recipe = new()
        {
            Name = "New recipe",
            Ingredients = [
                new() {
                    Name = "Eggs",
                    Quantity = new() { Amount = 1, }
                }
            ]
        };

        await Assert.ThrowsAsync<ApplicationException>(
                async () => await sut.UpdateRecipe(recipe));
    }

    [Fact]
    public async void UpdateRecipeThrowsIfRecipeNotFound()
    {
        var recipeRepository = new Mock<IRecipeRepository>();
        var ingredientRepository = new Mock<IIngredientRepository>();
        var foodRepository = new Mock<IFoodRepository>();
        var quantityMathService = new Mock<IQuantityService>();

        string id = "made_up_id";
        recipeRepository.Setup(r => r.Get(testUserId, id)).ReturnsAsync((Recipe?)null);

        RecipeService sut = new(_serviceProvider.Object, recipeRepository.Object,
                                    ingredientRepository.Object,
                                    foodRepository.Object,
                                    quantityMathService.Object);

        RecipeDto recipe = new()
        {
            Id = id,
            Name = "New recipe",
            Ingredients = [
                new() {
                    Name = "Eggs",
                    Quantity = new() { Amount = 1, }
                }
            ]
        };

        await Assert.ThrowsAsync<ApplicationException>(
            async () => await sut.UpdateRecipe(recipe));
    }

}
