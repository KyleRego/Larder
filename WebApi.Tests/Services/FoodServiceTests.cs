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
        FoodDto result = await sut.EatServings(dto);

        // assert
        Assert.Equal(3, result.Servings);

        mockFoodRepo.Verify(_ => _.Update(It.Is<Food>(f =>
            f.Servings == 3
        )), Times.Once);

        mockConsFoodRepo.Verify(_ => _.Insert(It.Is<ConsumedFood>(cf =>
            cf.FoodName == food.Name &&
            cf.CaloriesConsumed == food.Calories * dto.Servings &&
            cf.GramsProteinConsumed == food.GramsProtein * dto.Servings
        )), Times.Once);
    }
}
