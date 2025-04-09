using Larder.Models;

namespace Larder.Tests.Services.ContainerServiceTests;

public class RemoveItemFromContainerTests : ContainerServiceTestsBase
{
    [Fact]
    public async void RemoveItemFromContainer()
    {
        string userId = testUserId;
        string itemId = "black-pencil";
        string containerId = "backpack";

        await _sut.RemoveItemFromContainer(containerId, itemId);

        Item container = (await _itemData.Get(userId, containerId))!;
        Item? pencil = container.Container!.Items.FirstOrDefault(i =>
            i.Id == "pencil"
        );

        Assert.Null(pencil);
    }
}