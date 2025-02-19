using Larder.Models;
using Larder.Models.Builders;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Mocks.Repository;

public class MockIngredientData : MockRepositoryBase, IIngredientRepository
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

        _ingredients = [butter, rice];
    }

    public Task Delete(Item entity)
    {
        throw new NotImplementedException();
    }

    public Task<Item> FindOrCreateBy(string userId, string name)
    {
        throw new NotImplementedException();
    }

    public Task<Item?> Get(string userId, string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Item>> GetAll(string userId, IngredientSortOptions sortBy, string? search)
    {
        throw new NotImplementedException();
    }

    public Task<Item> Insert(Item newEntity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Item>> InsertAll(List<Item> newEntities)
    {
        throw new NotImplementedException();
    }

    public Task<Item> Update(Item editedEntity)
    {
        throw new NotImplementedException();
    }
}