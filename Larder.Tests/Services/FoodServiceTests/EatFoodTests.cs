using Larder.Dtos;
using Larder.Models;
using Larder.Services.Impl;
using Larder.Tests.Mocks.Repository;

namespace Larder.Tests.Services.FoodServiceTests;

public class EatFoodTests : ServiceTestsBase
{
    private readonly MockUnitData _unitData;
    private readonly MockUnitConversionData _unitConversionData;
    private readonly MockFoodData _foodData;
    private readonly UnitService _unitService;
    private readonly UnitConversionService _unitConversionService;
    private readonly QuantityService _quantityService;

    public EatFoodTests()
    {
        _unitData = new MockUnitData();
        _unitConversionData = new MockUnitConversionData(_unitData);
        _foodData = new MockFoodData(_unitData);
        _unitService = new(_serviceProvider.Object, _unitData);
        _unitConversionService = new(_serviceProvider.Object, _unitData, _unitConversionData);
        _quantityService = new(_serviceProvider.Object, _unitService, _unitConversionService);
    }

    [Fact]
    public async void EatApple()
    {
        EatFoodDto dto = new()
        {
            ItemId = "apples",
            QuantityEaten = new() { Amount = 1 }
        };

        QuantityDto expectedLeftOverApples = new() { Amount = 3 };

        FoodService sut = new(_serviceProvider.Object, _quantityService, _foodData);

        (ItemDto applesLeftOver, ItemDto eatenApple) = await sut.EatFood(dto);

        Assert.Equal(expectedLeftOverApples.Amount, applesLeftOver.Quantity?.Amount);

        Assert.Equal("Apples", eatenApple.Name);

        Item eatenFood = (await _foodData.Get(testUserId, eatenApple.Id!))!;

        Assert.NotNull(eatenFood.ConsumedTime);
        Assert.NotNull(eatenFood.Nutrition);

        Assert.Equal(1, eatenFood.Quantity.Amount);
        Assert.Equal(1, eatenFood.Nutrition.ServingSize.Amount);
        Assert.Equal(100, eatenFood.Nutrition.Calories);
        Assert.Equal(2, eatenFood.Nutrition.GramsProtein);
    }

    [Fact]
    public async void EatBreadSlices()
    {
        Unit breadSlices = (await _unitData.Get(testUserId, "bread-slices"))!;

        QuantityDto quantityToEat = new() { Amount = 3, UnitId = breadSlices.Id };

        EatFoodDto dto = new()
        {
            ItemId = "wheat-bread",
            QuantityEaten = quantityToEat
        };

        QuantityDto expectedLeftOverBread = new() { Amount = 18, UnitId = breadSlices.Id };

        FoodService sut = new(_serviceProvider.Object, _quantityService, _foodData);

        (ItemDto leftOverBread, ItemDto eatenBread) = await sut.EatFood(dto);

        // Assert expected quantities
        Assert.Equal(expectedLeftOverBread.Amount, leftOverBread.Quantity?.Amount);
        Assert.Equal(expectedLeftOverBread.UnitId, leftOverBread.Quantity?.UnitId);
        Assert.Equal(quantityToEat.Amount, eatenBread.Quantity?.Amount);
        Assert.Equal(quantityToEat.UnitId, eatenBread.Quantity?.UnitId);
    
        // Assert expected nutrition on eaten bread
        Assert.NotNull(eatenBread.Nutrition);
        NutritionDto nutrition = eatenBread.Nutrition;
        
        Assert.Equal(nutrition.ServingSize.Amount, quantityToEat.Amount);
        Assert.Equal(nutrition.ServingSize.UnitId, quantityToEat.UnitId);
        Assert.Equal(3 * 60, nutrition.Calories);
        Assert.Equal(3 * 1, nutrition.GramsTotalFat);
        Assert.Equal(0, nutrition.GramsSaturatedFat);
        Assert.Equal(0, nutrition.GramsSaturatedFat);
        Assert.Equal(3 * 100, nutrition.MilligramsSodium);
        Assert.Equal(3 * 3, nutrition.GramsProtein);
        Assert.Equal(2 * 3, nutrition.GramsDietaryFiber);
        Assert.Equal(1 * 3, nutrition.GramsTotalSugars);
        Assert.Equal(12 * 3, nutrition.GramsTotalCarbs);
    }
}
