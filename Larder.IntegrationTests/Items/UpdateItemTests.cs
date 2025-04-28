namespace Larder.IntegrationTests.Items;

public class UpdateItemTests(TestAppFactory<Program> factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task PutItems_UpdatesItem()
    {
        Item item = new ItemBuilder(_testUser.Id, "Basic item").Build();
        _dbContext.Add(item);
        await _dbContext.SaveChangesAsync();

        ItemDto updatePayload = new()
        {
            Id = item.Id,
            Name = "Updated item name"
        };

        HttpResponseMessage response = await _client.PutAsJsonAsync($"api/Items/{item.Id}", updatePayload);

        response.EnsureSuccessStatusCode();

        ApiResponse<ItemDto>? itemResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ItemDto>>();
        Assert.NotNull(itemResponse);
        Assert.NotNull(itemResponse.Data);
        Assert.Equal("Updated item name", itemResponse.Data.Name);
    }
}