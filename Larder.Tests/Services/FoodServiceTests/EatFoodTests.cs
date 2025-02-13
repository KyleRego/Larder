using Larder.Dtos;
using Larder.Models;
using Larder.Services.Impl;
using Larder.Tests.Mocks.Repository;

namespace Larder.Tests.Services.FoodServiceTests;

public class EatFoodTests : ServiceTestsBase
{
    private readonly MockFoodData _foodData = new();
    private readonly MockUnitData _unitData = new();
    private readonly MockUnitConversionData _unitConversionData = new();
    private readonly UnitService _unitService;
    private readonly UnitConversionService _unitConversionService;
    private readonly QuantityService _quantityService;

    public EatFoodTests()
    {
        _unitService = new(mSP.Object, _unitData);
        _unitConversionService = new(mSP.Object, _unitData, _unitConversionData);
        _quantityService = new(mSP.Object, _unitService, _unitConversionService);
    }

    [Fact]
    public async void EatApple()
    {
        EatFoodDto dto = new()
        {
            ItemId = "apples",
            QuantityEaten = new() { Amount = 1 }
        };

        QuantityDto expectedNewQuantity = new() { Amount = 3 };

        FoodService sut = new(mSP.Object, _quantityService, _foodData);

        (ItemDto foodLeftOver, ItemDto consumedFood) = await sut.EatFood(dto);

        Assert.Equal(expectedNewQuantity.Amount, foodLeftOver.Quantity?.Amount);

        Assert.Equal("apples", consumedFood.Name);

        Item eatenFood = (await _foodData.Get(mockUserId, consumedFood.Id!))!;

        Assert.NotNull(eatenFood.ConsumedTime);
        Assert.NotNull(eatenFood.Nutrition);

        Assert.Equal(1, eatenFood.Quantity.Amount);
        Assert.Equal(1, eatenFood.Nutrition.ServingSize.Amount);
        Assert.Equal(100, eatenFood.Nutrition.Calories);
        Assert.Equal(2, eatenFood.Nutrition.GramsProtein);
    }
}
