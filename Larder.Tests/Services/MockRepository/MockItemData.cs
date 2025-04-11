using Larder.Models;
using Larder.Models.Builders;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Services.MockRepository;

public class MockItemData : MockRepositoryBase, IItemRepository
{
    protected readonly IUnitRepository _unitData;
    protected readonly List<Item> _items = [];

    public MockItemData(IUnitRepository unitData)
    {
        _unitData = unitData;

        Unit butterSticks = Helpers.Untask(
            _unitData.Get(testUserId, "butter-sticks"));
        Unit grams = Helpers.Untask(
            _unitData.Get(testUserId, "grams"));      
        Unit breadSlices = Helpers.Untask(
            _unitData.Get(testUserId, "bread-slices"));

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

        Item pencil = new ItemBuilder(testUserId, "Black pencil")
                        .WithId("black-pencil").Build();
        Item notebook = new ItemBuilder(testUserId, "Composition notebook")
                        .WithId("composition-notebook").Build();
        Item backpack = new ItemBuilder(testUserId, "Backpack")
                .WithId("backpack")
                .WithContainedItems([pencil, notebook]).Build();

        _items.AddRange([
            apples, peanutButter, wheatBread, butter, rice, chickenLegQuarters,
            pencil, notebook, backpack
        ]);
    }

    public Task Delete(Item entity)
    {
        throw new NotImplementedException();
    }

    public Task<Item> FindOrCreate(string userId, string name)
    {
        throw new NotImplementedException();
    }

    public Task<Item?> GetOrNull(string userId, string id)
    {
        Item? item = _items.FirstOrDefault(item =>
            item.Id == id && item.UserId == userId);

        return Task.FromResult(item);
    }

    public Task<Item> Get(string userId, string id)
    {
        return GetOrNull(userId, id)!;
    }

    public Task<List<Item>> GetAll(string userId,
                                ItemSortOptions sortOption = ItemSortOptions.AnyOrder,
                                string? search = null)
    {
        return Task.FromResult<List<Item>>([.. _items]);
    }

    public Task<List<Item>> GetAllContainers(string userId)
    {
        return Task.FromResult<List<Item>>(
            [.. _items.Where(item => item.Container != null)]);
    }

    public Task<List<Item>> GetAllFoods(
        string userId, FoodSortOptions sortOption = FoodSortOptions.AnyOrder,
                                    string? search = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<Item>> GetAllIngredients(string userId,
                                    IngredientSortOptions sortOption = IngredientSortOptions.AnyOrder,
                                    string? search = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<Item>> GetConsumedFoods(string userId, DateTime day)
    {
        throw new NotImplementedException();
    }

    public Task<Item> Insert(Item newEntity)
    {
        _items.Add(newEntity);

        return Task.FromResult(newEntity);
    }

    public Task<List<Item>> InsertAll(List<Item> newEntities)
    {
        throw new NotImplementedException();
    }

    public Task<Item> Update(Item editedEntity)
    {
        return Task.FromResult(editedEntity);
    }
}
