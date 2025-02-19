using Larder.Models;
using Larder.Models.Builders;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Mocks.Repository;

public class MockFoodData : MockItemRepository, IFoodRepository
{
    public MockFoodData()
    {
        MockUnitData unitData = new();
        Unit grams = unitData.Get(testUserId, "grams").GetAwaiter().GetResult()!;
        Unit breadSlices = unitData.Get(testUserId, "bread-slices").GetAwaiter().GetResult()!;

        Item apples = new ItemBuilder(testUserId, "Apples")
            .WithId("apples")
            .WithQuantity(4)
            .WithNutrition(new NutritionBuilder()
                .WithCalories(100)
                .WithProtein(2))
            .Build();

        Item peanutButter = new ItemBuilder(testUserId, "Peanut Butter")
            .WithId("peanut-butter")
            .WithQuantity(189, grams)
            .WithNutrition(new NutritionBuilder()
                    .WithCalories(190)
                    .WithProtein(2))
            .Build();

        Item wheatBread = new ItemBuilder(testUserId, "Wheat bread")
            .WithId("wheat-bread")
            .WithQuantity(21, breadSlices)
            .WithNutrition(new NutritionBuilder()
                    .WithServingSize(1, breadSlices)
                    .WithCalories(60)
                    .WithTotalFat(1)
                    .WithSaturatedFat(0)
                    .WithTransFat(0)
                    .WithCholesterol(0)
                    .WithSodium(100)
                    .WithTotalCarbs(12)
                    .WithDietaryFiber(2)
                    .WithTotalSugars(1)
                    .WithProtein(3))
            .Build();

        _items.AddRange([apples, peanutButter, wheatBread]);
    }

    public Task<List<Item>> GetAll(string userId,
                FoodSortOptions sortOption = FoodSortOptions.AnyOrder,
                string? search = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<Item>> GetConsumedFoods(string userId, DateTime day)
    {
        throw new NotImplementedException();
    }
}