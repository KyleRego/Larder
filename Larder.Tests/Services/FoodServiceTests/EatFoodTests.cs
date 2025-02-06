using Larder.Dtos;
using Larder.Models;
using Larder.Models.ItemComponents;
using Larder.Repository.Interface;
using Larder.Services.Impl;
using Larder.Services.Interface;
using Larder.Tests.Repository;

namespace Larder.Tests.Services.FoodServiceTests;

public class EatFoodTests : ServiceTestsBase
{
    private readonly Mock<IQuantityMathService> _mockQuantMathService = new();

    [Fact]
    public async void EatAppleDecreasesAmount()
    {
        MockFoodRepository foodData = new();
        Item apples = await foodData.Get(mockUserId, "apples")
            ?? throw new ApplicationException("Data missing");

        EatFoodDto dto = new()
        {
            ItemId = "apples",
            QuantityEaten = new() { Amount = 1 }
        };

        Quantity expectedNewQuantity = new() { Amount = 3 };

        _mockQuantMathService.Setup(
            m => m.Subtract(It.IsAny<Quantity>(), It.IsAny<Quantity>())
        ).ReturnsAsync(expectedNewQuantity);

        FoodService sut = new(mSP.Object,
                                    _mockQuantMathService.Object,
                                    foodData);

        ItemDto foodAfterEat = await sut.EatFood(dto);

        Assert.Equal(expectedNewQuantity.Amount, foodAfterEat.Quantity?.Amount);
    }
}
