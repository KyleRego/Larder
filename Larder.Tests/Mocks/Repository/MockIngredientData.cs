using Larder.Models;
using Larder.Models.Builders;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Mocks.Repository;

public class MockIngredientData
    : MockItemRepository, IIngredientRepository
{
    public MockIngredientData(MockUnitData unitData) : base(unitData)
    {
        Unit butterSticks = _unitData.Get(testUserId, "butter-sticks")
                                    .GetAwaiter().GetResult()!;
        Unit grams = _unitData.Get(testUserId, "grams")
                                    .GetAwaiter().GetResult()!;
        // TODO: Add MockUnitConversion repository and conversion
        // between grams and butter sticks
        Item butter = new ItemBuilder(testUserId, "Butter")
                        .WithId("butter")
                        .WithQuantity(4, butterSticks)
                        .WithNutrition(new NutritionBuilder()
                            .WithCalories(100)
                            .WithTotalFat(11)
                            .WithTransFat(0)
                            .WithCholesterol(30)
                            .WithSodium(90)
                            .WithTotalCarbs(0)
                            .WithProtein(0)
                            .WithServingSize(14, grams))
                        .Build();
        Item rice = new ItemBuilder(testUserId, "Box rice")
                        .WithId("box-rice")
                        .WithQuantity(7 * 56, grams)
                        .WithNutrition(new NutritionBuilder()
                            .WithCalories(190)
                            .WithTotalCarbs(41)
                            .WithDietaryFiber(1)
                            .WithTotalSugars(1)
                            .WithProtein(5)
                            .WithSodium(730)
                            .WithServingSize(56, grams))
                        .Build();
        Item chickenLegQuarters = new ItemBuilder(testUserId, "Chicken leg quarters")
                        .WithId("chicken-leg-quarters")
                        .WithQuantity(4)
                        .WithNutrition(new NutritionBuilder()
                            .WithCalories(475)
                            .WithProtein(62)
                            .WithTotalFat(23)
                            .WithSaturatedFat(6.3)
                            .WithSodium(253))
                        .Build();

        _items.AddRange([butter, rice, chickenLegQuarters]);
    }

    public Task<List<Item>> GetAll(string userId,
                        IngredientSortOptions sortBy,
                        string? search)
    {
        throw new NotImplementedException();
    }
}