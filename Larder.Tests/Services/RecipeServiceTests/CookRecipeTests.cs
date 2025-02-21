using Larder.Dtos;
using Larder.Models;
using Larder.Services.Impl;
using Larder.Tests.Mocks.Repository;

namespace Larder.Tests.Services.RecipeServiceTests;

public class CookRecipeTests : RecipeServiceTestsBase
{
    [Fact]
    public async void CookChickenAndRice()
    {
        Unit tablespoons = UntaskResult(
            _unitData.Get(testUserId, "tablespoons"));
        Unit grams = UntaskResult(
            _unitData.Get(testUserId, "grams"));

        CookRecipeDto input = new()
        {
            RecipeId = "chicken-and-rice",
            ServingsProduced = 2,
            Ingredients = [
                new()
                {
                    IngredientItemId = "butter",
                    QuantityCooked = new()
                    {
                        Amount = 4,
                        UnitId = tablespoons.Id
                    }
                },
                new()
                {
                    IngredientItemId = "chicken-leg-quarters",
                    QuantityCooked = new()
                    {
                        Amount = 4
                    }
                },
                new()
                {
                    IngredientItemId = "box-rice",
                    QuantityCooked = new()
                    {
                        Amount = 7 * 56,
                        UnitId = grams.Id
                    }
                }
            ]
        };

        ItemDto result = await _sut.CookRecipe(input);

        Assert.Equal(input.ServingsProduced, result.Quantity!.Amount);
        Assert.Equal(1, result.Nutrition!.ServingSize.Amount);
    }
}
