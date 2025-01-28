using Larder.Dtos;
using Larder.Models;
using Larder.Models.ItemComponents;
using Larder.Repository;
using Larder.Services.Impl;

namespace Larder.Tests.Services;

public class FoodServiceTests : ServiceTestsBase
{
    private readonly string _foodItemId = "1";
    private readonly Dictionary<string, Item> _foodMap;
    private readonly Mock<IFoodRepository> _mockFoodRepo = new();

    public FoodServiceTests()
    {
        Item foodItem = new(mockUserId, "Apples")
        {
            Quantity = new() { Amount = 4 }
        };

        Nutrition food = new()
        {
            Item = foodItem,
            Calories = 100,
            GramsProtein = 2
        };
        foodItem.Food = food;

        _foodMap = [];
        _foodMap[_foodItemId] = foodItem;
        _mockFoodRepo.Setup(m => m.Get(mockUserId, _foodItemId)).ReturnsAsync(foodItem);
    }

    // [Fact]
    // public async void EatServings_CreatesAConsumedFoodAndDecreasesServingsOfFood()
    // {
    //     Item foodItem = _foodMap[_foodItemId];

    //     FoodServingsDto dto = new()
    //     {
    //         FoodId = "1",
    //         Servings = 1
    //     };
    //     FoodService sut = new(mSP.Object, _mockFoodRepo.Object);

    //     FoodDto foodDtoResult = await sut.EatFood(dto);

    //     double expectedCalories = dto.Servings * foodItem.Food!.Calories;
    //     double expectedProtein = dto.Servings * foodItem.Food.GramsProtein;

    //     Assert.Equal(3, foodDtoResult.Servings);

    //     _mockFoodRepo.Verify(_ => _.Update(It.Is<Item>(item =>
    //         item.Food!.Servings == 3
    //     )), Times.Once);
    // }

    // [Fact]
    // public async void EatFood_UpdatesFoodTotalProperties()
    // {
    //     Item foodItem = _foodMap[_foodItemId];
    //     foodItem.Food!.TotalCalories = 0;
    //     foodItem.Food.TotalGramsProtein = 0;

    //     _mockFoodRepo.Setup(m => m.Get(mockUserId, foodItem.Id)).ReturnsAsync(foodItem);

    //     FoodService sut = new(mSP.Object, _mockFoodRepo.Object);

    //     FoodServingsDto dto = new()
    //     {
    //         FoodId = foodItem.Id,
    //         Servings = 1
    //     };

    //     FoodDto foodDto = await sut.EatFood(dto);

    //     Assert.Equal(foodDto.Calories * foodDto.Servings, foodDto.TotalCalories);
    //     Assert.Equal(foodDto.GramsProtein * foodDto.Servings, foodDto.TotalGramsProtein);
    // }
}
