using Larder.Models;

namespace Larder.Tests.Repository.ItemRepositoryTests;

public class GetAllFoodsTests : ItemRepositoryTestsBase
{
    [Fact]
    public async void GetAllFoodsDoesNotReturnConsumedFoods()
    {
        List<Item> result = await _sut.GetAllFoods(testUserId);

        Assert.Equal(_numFoods, result.Count);

        foreach (Item item in result)
        {
            Assert.Null(item.ConsumedTime);
        }
    }
}