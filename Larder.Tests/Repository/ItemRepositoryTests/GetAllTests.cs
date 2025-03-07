using Larder.Models;

namespace Larder.Tests.Repository.ItemRepositoryTests;

public class GetAllTests : ItemRepositoryTestsBase
{
    [Fact]
    public async void GetAllDoesNotIncludeConsumedFoods()
    {
        List<Item> result = await _sut.GetAll(testUserId);

        Assert.Equal(_numTotalItems, result.Count);

        foreach (Item item in result)
        {
            Assert.Null(item.ConsumedTime);
        }
    }
}