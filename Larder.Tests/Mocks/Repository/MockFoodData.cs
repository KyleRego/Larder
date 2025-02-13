using Larder.Models;
using Larder.Models.Builders;
using Larder.Models.ItemComponents;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Mocks.Repository;

public class MockFoodData : MockRepositoryBase, IFoodRepository
{
    private readonly List<Item> foodItems = [];

    public MockFoodData()
    {
        Item apples = new ItemBuilder(testUserId, "Apples")
            .WithQuantity(4)
            .WithNutrition(100, 2)
            .Build();

        Unit grams = new(testUserId, "grams", UnitType.Mass);

        Item peanutButter = new ItemBuilder(testUserId, "Peanut Butter")
            .WithQuantity(189, grams)
            .WithNutrition(190, 8)
            .Build();

        foodItems.AddRange([apples, peanutButter]);
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
        Item? food = foodItems.FirstOrDefault(item =>
            item.Id == id && item.UserId == userId);

        return Task<Item?>.FromResult(food);
    }

    public Task<List<Item>> GetAll(string userId, FoodSortOptions sortBy, string? search)
    {
        throw new NotImplementedException();
    }

    public Task<List<Item>> GetConsumedFoods(string userId, DateTime day)
    {
        throw new NotImplementedException();
    }

    public Task<Item> Insert(Item newEntity)
    {
        foodItems.Add(newEntity);

        return Task<Item>.FromResult(newEntity);
    }

    public Task<List<Item>> InsertAll(List<Item> newEntities)
    {
        throw new NotImplementedException();
    }

    public Task<Item> Update(Item editedEntity)
    {
        return Task<Item>.FromResult(editedEntity);
    }
}