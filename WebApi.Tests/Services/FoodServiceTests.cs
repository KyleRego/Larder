using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Larder.Services;

namespace Larder.Tests.Services;

public class FoodServiceTests
{
    private readonly Dictionary<string, Food> _foodMap;

    public FoodServiceTests()
    {
        _foodMap = [];

        _foodMap["1"] = new()
        {
            Id = "1",
            Servings = 4,
            Name = "Apple",
            Calories = 100,
            GramsProtein = 2
        };
    }

    [Fact]
    public async void EatServingsCreatesAConsumedFoodAndDecreasesServingsOfFood()
    {
        // arrange
        string foodId = "1";
        Food food = _foodMap[foodId];

        var mockFoodRepo = new Mock<IFoodRepository>();
        mockFoodRepo.Setup(m => m.Get(foodId)).ReturnsAsync(food);

        var mockConsFoodRepo = new Mock<IConsumedFoodRepository>();
        mockConsFoodRepo.Setup(m => m.Insert(It.IsAny<ConsumedFood>()));

        FoodServingsDto dto = new()
        {
            FoodId = "1",
            Servings = 1
        };
        FoodService sut = new(mockFoodRepo.Object, mockConsFoodRepo.Object);

        // act
        (FoodDto foodDtoResult, ConsumedFoodDto consumedFoodDtoResult) = await sut.EatFood(dto);

        // assert
        double expectedCalories = dto.Servings * food.Calories;
        double expectedProtein = dto.Servings * food.GramsProtein;

        Assert.Equal(3, foodDtoResult.Servings);
        Assert.Equal(food.Name, consumedFoodDtoResult.Name);
        Assert.Equal(expectedCalories, consumedFoodDtoResult.Calories);
        Assert.Equal(expectedProtein, consumedFoodDtoResult.GramsProtein);

        mockFoodRepo.Verify(_ => _.Update(It.Is<Food>(f =>
            f.Servings == 3
        )), Times.Once);

        mockConsFoodRepo.Verify(_ => _.Insert(It.Is<ConsumedFood>(cf =>
            cf.FoodName == food.Name &&
            cf.CaloriesConsumed == expectedCalories &&
            cf.GramsProteinConsumed == expectedProtein
        )), Times.Once);
    }
}
