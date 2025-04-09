using Larder.Models;

namespace Larder.Tests.Repository.ItemRepositoryTests;

public class GetAllContainersTests : ItemRepositoryTestsBase
{
    [Fact]
    public async void GetAllContainersReturnsContainers()
    {
        List<Item> result = await _sut.GetAllContainers(testUserId);

        Assert.Equal(_numContainers, result.Count);

        foreach (Item item in result)
        {
            Assert.Null(item.ConsumedTime);
        }
    }
}