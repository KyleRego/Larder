using Larder.Dtos;
using Larder.Models;
using Larder.Repository.Interface;
using Larder.Services.Impl;
using Larder.Services.Interface;
using Larder.Tests.TestData;

namespace Larder.Tests.Services.RecipeServiceTests;

public class CookRecipeTests : ServiceTestsBase
{
    [Fact]
    public async Task CookChickenAndRice()
    {
        string userId = TestUser.TestUserId();
        Unit tablespoons = TestUnits.TableSpoons();

        Recipe recipe = new(userId, "Chicken and rice")
        {
            ServingsProduced = 1
        };
        Item foodItemCreated = new(userId, recipe.Name)
        {
            Quantity = new() { Amount = 1 }
        };
        foodItemCreated.Nutrition = new()
        {
            Item = foodItemCreated
        };

        Item rice = new(userId, "Family size rice roni box")
        {
            Quantity = new() { Amount = 2 }
        };
        rice.Ingredient = new()
        {
            Item = rice,
            Recipes = [recipe]
        };

        Item butter = new(userId, "Butter")
        {
            Quantity = new() { Amount = 1 }
        };
        butter.Ingredient = new()
        {
            Item = butter,
            Recipes = [recipe]
        };
        butter.Quantity = new()
        {
            UnitId = tablespoons.Id,
            Unit = tablespoons,
            Amount = 16
        };

        RecipeIngredient riceRecIng = new(userId, recipe.Id, rice.Id)
        {
            Quantity = new() { Amount = 1 },
            Ingredient = rice.Ingredient
        };

        RecipeIngredient butRecIng = new(userId, recipe.Id, butter.Id)
        {
            Quantity = new()
            {
                Amount = 4,
                Unit = tablespoons,
                UnitId = tablespoons.Id
            },
            Ingredient = butter.Ingredient
        };

        recipe.RecipeIngredients = [riceRecIng, butRecIng];

        CookRecipeDto input = new()
        {
            RecipeId = recipe.Id
        };

        var mockRecipeData = new Mock<IRecipeRepository>();
        mockRecipeData.Setup(
            m => m.Get(userId, recipe.Id)).ReturnsAsync(recipe);

        var mockQuantMathService = new Mock<IQuantityMathService>();
        mockQuantMathService.Setup(
            m => m.Subtract(It.IsAny<Quantity>(), riceRecIng.Quantity))
            .ReturnsAsync((Quantity)
                new() { Amount = 1 });
        mockQuantMathService.Setup(
            m => m.Subtract(butter.Quantity, butRecIng.Quantity))
            .ReturnsAsync((Quantity)
                new()
                {
                    Amount = 12,
                    Unit = tablespoons,
                    UnitId = tablespoons.Id
                });

        var mockFoodData = new Mock<IFoodRepository>();
        mockFoodData.Setup(
            m => m.FindOrCreateBy(userId, recipe.Name))
                    .ReturnsAsync(foodItemCreated);

        // This assertion can be changed once the domain logic is
        // more fleshed out ie a food item created could have
        // variable servings created based on the amount of food
        // cooked as an argument to the recipe

        var mockIngredientData = new Mock<IIngredientRepository>();

        RecipeService sut = new(mSP.Object,
                                mockRecipeData.Object,
                                mockIngredientData.Object,
                                mockFoodData.Object,
                                mockQuantMathService.Object);

        await sut.CookRecipe(input);

        // Assert.Equal(foodItemCreated.Food!.Servings, recipe.ServingsProduced);
    }
}
