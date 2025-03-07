using Larder.Models;

namespace Larder.Tests.Repository.ItemRepositoryTests;

public class GetConsumedFoodsTests : ItemRepositoryTestsBase
{
    [Fact]
    public async void TestConsumedFoodsHaveConsumedAtProperty()
    {
        List<Item> result = await _sut.GetConsumedFoods(testUserId, _foodsEatenTime.Date);

        Assert.Equal(_numEatenFoods, result.Count);

        foreach (Item eatenFood in result)
        {
            Assert.NotNull(eatenFood.ConsumedTime);
        }
    }
}
