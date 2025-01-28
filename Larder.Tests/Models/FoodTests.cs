using Larder.Dtos;
using Larder.Models;
using Larder.Models.ItemComponents;

namespace Larder.Tests.Models;

public class FoodTests
{
    private readonly string userId = "1";

    [Fact]
    public void FromDtoCreatesFoodEntity()
    {
        Item item = new(userId, "Test item")
        {
            Quantity = new() { Amount = 1 }
        };
        NutritionDto dto = new()
        {
            Servings = 11,
            ServingSize = new() { Amount = 363, UnitId = null},
            Calories = 17,
            GramsProtein = 19,

            GramsTotalFat = 233,
            GramsSaturatedFat = 3,
            GramsTransFat = 29,

            GramsDietaryFiber = 2,

            GramsTotalCarbs = 283,
            GramsTotalSugars = 284,

            TotalGramsProtein = 0,
            TotalCalories = 0
        };

        Nutrition food = Nutrition.FromDto(dto, item);

        Assert.Equal(363, food.ServingSize.Amount);
        Assert.Equal(17, food.Calories);
        Assert.Equal(19, food.GramsProtein);
        Assert.Equal(233, food.GramsTotalFat);
        Assert.Equal(3, food.GramsSaturatedFat);
        Assert.Equal(29, food.GramsTransFat);
        Assert.Equal(2, food.GramsDietaryFiber);
        Assert.Equal(283, food.GramsTotalCarbs);
        Assert.Equal(284, food.GramsTotalSugars);
    }
}