using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Larder.Services;

namespace Larder.Tests.Services;

public class FoodServiceTests : ServiceTestsBase
{
    private readonly string _foodItemId = "1";
    private readonly Dictionary<string, Item> _foodMap;
    private readonly Mock<IFoodRepository> _mockFoodRepo = new();
    private readonly Mock<IConsumedFoodRepository> _mockConsFoodRepo = new();

    public FoodServiceTests()
    {
        Item foodItem = new()
        {
            Id = _foodItemId,
            Name = "Apples",
            UserId = mockUserId
        };

        Food food = new()
        {
            Item = foodItem,
            Servings = 4,
            Calories = 100,
            GramsProtein = 2,
            TotalCalories = 400,
            TotalGramsProtein = 8
        };
        foodItem.Food = food;

        _foodMap = [];
        _foodMap[_foodItemId] = foodItem;
        _mockFoodRepo.Setup(m => m.Get(_foodItemId)).ReturnsAsync(foodItem);
    }

    [Fact]
    public async void EatServings_CreatesAConsumedFoodAndDecreasesServingsOfFood()
    {
        Item foodItem = _foodMap[_foodItemId];

        _mockConsFoodRepo.Setup(m => m.Insert(It.IsAny<ConsumedFood>()));

        FoodServingsDto dto = new()
        {
            FoodId = "1",
            Servings = 1
        };
        FoodService sut = new(mSP.Object, _mockFoodRepo.Object, _mockConsFoodRepo.Object);

        (FoodDto foodDtoResult, ConsumedFoodDto consumedFoodDtoResult) = await sut.EatFood(dto);

        double expectedCalories = dto.Servings * foodItem.Food!.Calories;
        double expectedProtein = dto.Servings * foodItem.Food.GramsProtein;

        Assert.Equal(3, foodDtoResult.Servings);
        Assert.Equal(foodItem.Name, consumedFoodDtoResult.Name);
        Assert.Equal(expectedCalories, consumedFoodDtoResult.Calories);
        Assert.Equal(expectedProtein, consumedFoodDtoResult.GramsProtein);

        _mockFoodRepo.Verify(_ => _.Update(It.Is<Item>(item =>
            item.Food!.Servings == 3
        )), Times.Once);

        _mockConsFoodRepo.Verify(_ => _.Insert(It.Is<ConsumedFood>(cf =>
            cf.FoodName == foodItem.Name &&
            cf.CaloriesConsumed == expectedCalories &&
            cf.GramsProteinConsumed == expectedProtein
        )), Times.Once);
    }

    [Fact]
    public async void EatFood_UpdatesFoodTotalProperties()
    {
        Item foodItem = _foodMap[_foodItemId];
        foodItem.Food!.TotalCalories = 0;
        foodItem.Food.TotalGramsProtein = 0;

        _mockFoodRepo.Setup(m => m.Get(_foodItemId)).ReturnsAsync(foodItem);

        FoodService sut = new(mSP.Object, _mockFoodRepo.Object,
                                                    _mockConsFoodRepo.Object);

        FoodServingsDto dto = new()
        {
            FoodId = foodItem.Id,
            Servings = 1
        };

        (FoodDto foodDto, ConsumedFoodDto consFoodDto) = await sut.EatFood(dto);

        Assert.Equal(foodDto.Calories * foodDto.Servings, foodDto.TotalCalories);
        Assert.Equal(foodDto.GramsProtein * foodDto.Servings, foodDto.TotalGramsProtein);
    }
}
