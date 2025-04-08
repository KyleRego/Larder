using Larder.Dtos;

namespace Larder.Tests.Services.ItemServiceTests;

public class UpdateTests : ItemServiceTestsBase
{
    [Fact]
    public async void UpdateNonContainerToContainer()
    {
        ItemDto bag = new()
        {
            Name = "Backpack"
        };

        ItemDto testItem = await _sut.Add(bag);
        testItem.IsContainer = true;

        ItemDto result = await _sut.Update(testItem);
        Assert.True(result.IsContainer);
    }

    [Fact]
    public async void UpdateContainerItemDoesNotClearContainedItems()
    {
        ItemDto updateBackpackDto = new()
        {
            Id = "backpack",
            Name = "Backpack (updated)",
            IsContainer = true
        };

        ItemDto result = await _sut.Update(updateBackpackDto);

        Assert.Equal(updateBackpackDto.Name, result.Name);
        Assert.True(result.IsContainer);
        Assert.NotEmpty(result.ContainedItems);
    }

    [Fact]
    public async void UpdateContainerToNonContainer()
    {
        ItemDto updateBackpackDto = new()
        {
            Id = "backpack",
            Name = "Backpack (updated)",
            IsContainer = false
        };

        ItemDto result = await _sut.Update(updateBackpackDto);

        Assert.Equal(updateBackpackDto.Name, result.Name);
        Assert.False(result.IsContainer);
        Assert.Empty(result.ContainedItems);
    }
}