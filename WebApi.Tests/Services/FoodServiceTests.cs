using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Larder.Services;

namespace Larder.Tests.Services;

public class FoodServiceTests : ServiceTestsBase
{
    private readonly string _foodId = "1";
    private readonly Dictionary<string, Food> _foodMap;
    private readonly Mock<IFoodRepository> _mockFoodRepo = new();
    private readonly Mock<IConsumedFoodRepository> _mockConsFoodRepo = new();

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
            GramsProtein = 2,
            TotalCalories = 400,
            TotalGramsProtein = 8
        };
    }

    [Fact]
    public async void CreateFoodSetsTotalsOfCaloriesAndProteinFromServings()
    {
        FoodService sut = new(_mockFoodRepo.Object, _mockConsFoodRepo.Object,
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

        _mockFoodRepo.Setup(m => m.Get(_foodId)).ReturnsAsync(foodToUpdate);

        FoodService sut = new(_mockFoodRepo.Object, _mockConsFoodRepo.Object,
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

        _mockFoodRepo.Setup(m => m.Get(_foodId)).ReturnsAsync(food);
        _mockConsFoodRepo.Setup(m => m.Insert(It.IsAny<ConsumedFood>()));

        FoodServingsDto dto = new()
        {
            FoodId = "1",
            Servings = 1
        };
        FoodService sut = new(_mockFoodRepo.Object, _mockConsFoodRepo.Object,
                        mockHttpContextAccessor.Object, mockAuthorizationService.Object);

        (FoodDto foodDtoResult, ConsumedFoodDto consumedFoodDtoResult) = await sut.EatFood(dto);

        double expectedCalories = dto.Servings * food.Calories;
        double expectedProtein = dto.Servings * food.GramsProtein;

        Assert.Equal(3, foodDtoResult.Servings);
        Assert.Equal(food.Name, consumedFoodDtoResult.Name);
        Assert.Equal(expectedCalories, consumedFoodDtoResult.Calories);
        Assert.Equal(expectedProtein, consumedFoodDtoResult.GramsProtein);

        _mockFoodRepo.Verify(_ => _.Update(It.Is<Food>(f =>
            f.Servings == 3
        )), Times.Once);

        _mockConsFoodRepo.Verify(_ => _.Insert(It.Is<ConsumedFood>(cf =>
            cf.FoodName == food.Name &&
            cf.CaloriesConsumed == expectedCalories &&
            cf.GramsProteinConsumed == expectedProtein
        )), Times.Once);
    }

    [Fact]
    public async void EatFoodUpdatesFoodTotalProperties()
    {
        Food food = _foodMap[_foodId];
        double initialTotalCalories = food.TotalCalories;
        double initialGramsProtein = food.TotalGramsProtein;

        _mockFoodRepo.Setup(m => m.Get(_foodId)).ReturnsAsync(food);

        FoodService sut = new(_mockFoodRepo.Object, _mockConsFoodRepo.Object,
                        mockHttpContextAccessor.Object, mockAuthorizationService.Object);

        FoodServingsDto dto = new()
        {
            FoodId = food.Id,
            Servings = 1
        };

        (FoodDto foodDto, ConsumedFoodDto consFoodDto) = await sut.EatFood(dto);

        double caloriesConsumed = food.Calories * dto.Servings;
        double gramsProteinConsumed = food.GramsProtein * dto.Servings;
        Assert.Equal(initialTotalCalories - caloriesConsumed, foodDto.TotalCalories);
        Assert.Equal(initialGramsProtein - gramsProteinConsumed, foodDto.TotalGramsProtein);
    }
}
