using Larder.Dtos;

namespace Larder.Tests.Services.ItemServiceTests;

public class AddTests : ItemServiceTestsBase
{
    [Fact]
    public async void CreateBasicItem()
    {
        ItemDto dto = new()
        {
            Name = "Item",
            Description = "Item description"
        };

        int beforeCount = (await _sut.GetItems()).Count;

        ItemDto result = await _sut.Add(dto);
        int afterCount = (await _sut.GetItems()).Count;

        Assert.Equal(beforeCount + 1, afterCount);
        Assert.Equal(dto.Name, result.Name);
        Assert.Equal(dto.Description, result.Description);
        Assert.Null(result.Nutrition);
        Assert.False(result.IsContainer);
    }

    [Fact]
    public async void CreateContainerItem()
    {
        ItemDto dto = new()
        {
            Name = "Backpack",
            Description = "Test container",
            IsContainer = true
        };

        ItemDto result = await _sut.Add(dto);

        Assert.True(result.IsContainer);
    }
}
