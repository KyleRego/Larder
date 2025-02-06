using Larder.Models;
using Larder.Models.ItemComponents;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Mocks.Repository;

public class MockFoodData : MockRepositoryBase, IFoodRepository
{
    private readonly List<Item> foodItems = [];

    public MockFoodData()
    {
        Item apples = new(testUserId, "apples")
        {
            Id = "apples",
            Quantity = new() { Amount = 4 }
        };
        Nutrition applesNutrition = new()
        {
            Item = apples,
            Calories = 100,
            GramsProtein = 2
        };
        apples.Nutrition = applesNutrition;

        Unit grams = new(testUserId, "grams", UnitType.Mass);

        Item peanutButter = new(testUserId, "Peanut Butter")
        {
            Id = "peanut-butter",
            Quantity = new() { Amount = 189, Unit = grams, UnitId = grams.Id }
        };

        Nutrition peanutButterNutrition = new()
        {
            Item = peanutButter,
            Calories = 190,
            GramsProtein = 8
        };
        peanutButter.Nutrition = peanutButterNutrition;

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

    public Task<List<Item>> GetConsumedFoods(string userId)
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