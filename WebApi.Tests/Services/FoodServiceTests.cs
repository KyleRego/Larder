using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Larder.Services;

namespace Larder.Tests.Services;

public class FoodServiceTests : ServiceTestsBase
{
    private readonly string _foodId = "1";
    private readonly Dictionary<string, Food> _foodMap;

    public FoodServiceTests()
    {
        _foodMap = [];

        _foodMap[_foodId] = new()
        {
            UserId = mockUserId,
            Id = _foodId,
            Servings = 4,
            Name = "Apple",
            Calories = 100,
            GramsProtein = 2
        };
    }

    [Fact]
    public async void CreateFoodSetsTotalsOfCaloriesAndProteinFromServings()
    {
        var mockFoodRepo = new Mock<IFoodRepository>();
        var mockConsFoodRepo = new Mock<IConsumedFoodRepository>();

        FoodService sut = new(mockFoodRepo.Object, mockConsFoodRepo.Object,
                        mockHttpContextAccessor.Object, mockAuthorizationService.Object);

        double calories = 100;
        double proteins = 15;
        double servings = 4;

        FoodDto food = new()
        {
            Name = "Test food",
            Calories = calories,
            GramsProtein = proteins,
            Servings = servings
        };

        FoodDto result = await sut.CreateFood(food);

        Assert.Equal(calories * servings, result.TotalCalories);
        Assert.Equal(proteins * servings, result.TotalGramsProtein);
    }

    [Fact]
    public async void UpdateFoodUpdatesTotalsOfCaloriesAndProteinFromServings()
    {
        Food foodToUpdate = _foodMap[_foodId];

        var mockFoodRepo = new Mock<IFoodRepository>();
        mockFoodRepo.Setup(m => m.Get(_foodId)).ReturnsAsync(foodToUpdate);
        var mockConsFoodRepo = new Mock<IConsumedFoodRepository>();

        FoodService sut = new(mockFoodRepo.Object, mockConsFoodRepo.Object,
                        mockHttpContextAccessor.Object, mockAuthorizationService.Object);

        double calories = 150;
        double proteins = 10;
        double servings = 3;

        FoodDto foodDto = new()
        {
            Id = foodToUpdate.Id,
            Name = foodToUpdate.Name,
            Calories = calories,
            GramsProtein = proteins,
            Servings = servings
        };

        FoodDto result = await sut.UpdateFood(foodDto);

        Assert.Equal(calories * servings, result.TotalCalories);
        Assert.Equal(proteins * servings, result.TotalGramsProtein);
    }

    [Fact]
    public async void EatServingsCreatesAConsumedFoodAndDecreasesServingsOfFood()
    {
        Food food = _foodMap[_foodId];

        var mockFoodRepo = new Mock<IFoodRepository>();
        mockFoodRepo.Setup(m => m.Get(_foodId)).ReturnsAsync(food);

        var mockConsFoodRepo = new Mock<IConsumedFoodRepository>();
        mockConsFoodRepo.Setup(m => m.Insert(It.IsAny<ConsumedFood>()));

        FoodServingsDto dto = new()
        {
            FoodId = "1",
            Servings = 1
        };
        FoodService sut = new(mockFoodRepo.Object, mockConsFoodRepo.Object,
                        mockHttpContextAccessor.Object, mockAuthorizationService.Object);

        (FoodDto foodDtoResult, ConsumedFoodDto consumedFoodDtoResult) = await sut.EatFood(dto);

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
