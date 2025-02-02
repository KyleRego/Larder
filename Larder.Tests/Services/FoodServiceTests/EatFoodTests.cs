using Larder.Dtos;
using Larder.Models;
using Larder.Models.ItemComponents;
using Larder.Repository.Interface;
using Larder.Services.Impl;
using Larder.Services.Interface;

namespace Larder.Tests.Services.FoodServiceTests;

public class EatFoodTests : ServiceTestsBase
{
    private readonly string _appliesId = "1";
    private readonly Dictionary<string, Item> _foodMap;
    private readonly Mock<IQuantityMathService> _mockQuantMathService = new();
    private readonly Mock<IFoodRepository> _mockFoodRepo = new();

    public EatFoodTests()
    {
        Item apples = new(mockUserId, "Apples")
        {
            Quantity = new() { Amount = 4 }
        };

        Nutrition applesNutrition = new()
        {
            Item = apples,
            Calories = 100,
            GramsProtein = 2
        };
        apples.Nutrition = applesNutrition;

        _foodMap = [];
        _foodMap[_appliesId] = apples;
        _mockFoodRepo.Setup(m => m.Get(mockUserId, _appliesId)).ReturnsAsync(apples);
    }

    [Fact]
    public async void EatFoodDecreasesItemQuantity()
    {
        Item foodItem = _foodMap[_appliesId];

        FoodServingsDto dto = new()
        {
            FoodId = _appliesId,
            QuantityEaten = new() { Amount = 1 }
        };

        _mockQuantMathService.Setup(
            m => m.Subtract(It.IsAny<Quantity>(), It.IsAny<Quantity>())
        ).ReturnsAsync(new Quantity { Amount = 3 });

        FoodService sut = new(mSP.Object,
                                    _mockQuantMathService.Object,
                                    _mockFoodRepo.Object);

        await sut.EatFood(dto);

        _mockFoodRepo.Verify(_ => _.Update(It.Is<Item>(item =>
            item != null && item.Quantity != null && item.Quantity.Amount == 3
        )), Times.Once);
    }
}
