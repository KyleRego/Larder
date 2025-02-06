using Larder.Dtos;
using Larder.Models;
using Larder.Models.Interface;
using Larder.Services.Impl;
using Larder.Services.Interface;
using Larder.Tests.Mocks.Repository;

namespace Larder.Tests.Services.FoodServiceTests;

public class EatFoodTests : ServiceTestsBase
{
    private readonly Mock<IQuantityMathService> _mockQuantMathService = new();

    [Fact]
    public async void EatApple()
    {
        MockFoodData foodData = new();
        Item apples = await foodData.Get(mockUserId, "apples")
            ?? throw new ApplicationException("Data missing");

        EatFoodDto dto = new()
        {
            ItemId = "apples",
            QuantityEaten = new() { Amount = 1 }
        };

        QuantityDto expectedNewQuantity = new() { Amount = 3 };

        _mockQuantMathService.Setup(
            m => m.SubtractUpToZero(It.IsAny<IQuantity>(), It.IsAny<IQuantity>())
        ).ReturnsAsync(expectedNewQuantity);

        FoodService sut = new(mSP.Object,
                                    _mockQuantMathService.Object,
                                    foodData);

        (ItemDto foodAfterEat, ItemDto consumedFood) = await sut.EatFood(dto);

        Assert.Equal(expectedNewQuantity.Amount, foodAfterEat.Quantity?.Amount);

        Assert.Equal("apples - Eaten", consumedFood.Name);
    }
}
