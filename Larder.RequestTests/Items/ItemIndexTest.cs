namespace Larder.RequestTests.Items;

public class ItemIndexTest(TestAppFactory<Program> factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task GetItems_ReturnsSeededItem()
    {
        await SeedItems();

        var response = await _client.GetAsync("/api/items");
        response.EnsureSuccessStatusCode();

        List<ItemDto>? items = await response.Content.ReadFromJsonAsync<List<ItemDto>>();
        Assert.NotNull(items);
        Assert.Equal(3, items.Count);
    }

    private async Task SeedItems()
    {
        Item item1 = new ItemBuilder(_testUser.Id, "Spoon").Build();
        Item item2 = new ItemBuilder(_testUser.Id, "Fork").Build();
        Item item3 = new ItemBuilder(_testUser.Id, "Plate").Build();

        await _dbContext.AddRangeAsync([item1, item2, item3]);
        await _dbContext.SaveChangesAsync();
    }
}
