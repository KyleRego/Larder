namespace Larder.RequestTests.Items;

public class CreateItemTest (TestAppFactory<Program> factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task PostItems_CreatesBasicItem()
    {
        ItemDto payload = new()
        {
            Name = "New basic item",
            Description = "Description"
        };

        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/items", payload);
        response.EnsureSuccessStatusCode();
        
        ApiResponse<ItemDto>? itemResponse = await response.Content.ReadFromJsonAsync<ApiResponse<ItemDto>>();
        Assert.NotNull(itemResponse);
        Assert.NotNull(itemResponse.Data);
        Assert.Equal("New basic item", itemResponse.Data.Name);
    }
}
