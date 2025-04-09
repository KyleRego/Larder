using Larder.Models;
using Larder.Models.Builders;

namespace Larder.Tests.Services.ContainerServiceTests;

public class PutItemInContainerTests : ContainerServiceTestsBase
{
    [Fact]
    public async void PutItemInContainer()
    {
        string userId = testUserId;
        string itemId = "apples";
        string containerId = "backpack";

        await _sut.PutItemInContainer(containerId, itemId);

        Item container = (await _itemData.Get(userId, containerId))!;
        Item? apples = container.Container!.Items.FirstOrDefault(i =>
            i.Id == "apples"
        );

        Assert.NotNull(apples);
    }
}