using Larder.Models;
using Larder.Models.Builders;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Mocks.Repository;

public class MockIngredientData : MockItemRepository, IIngredientRepository
{
    private readonly List<Item> _ingredients = [];

    public MockIngredientData()
    {
        MockUnitData unitData = new();
        Unit butterSticks = unitData.Get(testUserId, "butter-sticks")
                                    .GetAwaiter().GetResult()!;

        Item butter = new ItemBuilder(testUserId, "Butter")
                        .WithId("butter")
                        .WithQuantity(4, butterSticks)
                        .WithNutrition(new NutritionBuilder()
                            .WithCalories(810)
                            .WithServingSize(1, butterSticks))
                        .Build();
        Item rice = new ItemBuilder(testUserId, "Box rice")
                        .WithId("box-rice")
                        .WithQuantity(1)
                        .WithNutrition(new NutritionBuilder()
                            .WithCalories(500)
                            .WithServingSize(1))
                        .Build();

        _items.AddRange([butter, rice]);
    }

    public Task<List<Item>> GetAll(string userId,
                        IngredientSortOptions sortBy,
                        string? search)
    {
        throw new NotImplementedException();
    }
}