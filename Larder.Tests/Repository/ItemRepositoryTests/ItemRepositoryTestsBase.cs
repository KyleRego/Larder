using Larder.Models;
using Larder.Models.Builders;
using Larder.Repository.Impl;
using Larder.Repository.Interface;

namespace Larder.Tests.Repository.ItemRepositoryTests;

public abstract class ItemRepositoryTestsBase : RepositoryTestBase
{
    protected readonly IItemRepository _sut;
    protected DateTimeOffset _foodsEatenTime = DateTimeOffset.Now;
    protected int _numEatenFoods;
    protected int _numFoods;
    protected int _numTotalItems;

    public ItemRepositoryTestsBase()
    {
        SetupGenericItems();
        SetupFoods();
        SetupConsumedFoods();
        _sut = new ItemRepository(_dbContext);
    }

    private void SetupGenericItems()
    {
        Item item1 = new ItemBuilder(testUserId, "Pencil", "For writing")
                        .Build();
    
        Item[] genericItems = [item1];

        _numTotalItems += genericItems.Length;

        _dbContext.AddRange(genericItems);
        _dbContext.SaveChanges();
    }

    private void SetupFoods()
    {
        Item food1 = new ItemBuilder(testUserId, "Toaster pastries")
                        .WithNutrition(new NutritionBuilder()
                                            .WithCalories(120)
                                            .WithProtein(2))
                        .Build();

        Item[] foods = [food1];

        int numFoods = foods.Length;

        _numFoods = numFoods;
        _numTotalItems += numFoods;

        _dbContext.AddRange(foods);
        _dbContext.SaveChanges();
    }

    private void SetupConsumedFoods()
    {
        Item consumedFood1 = new ItemBuilder(testUserId, "Apples")
                                    .WithNutrition(new NutritionBuilder()
                                                .WithCalories(100))
                                    .WithConsumedTime(new ConsumedTimeBuilder()
                                                .WithTime(_foodsEatenTime))
                                    .Build();
        Item consumedFood2  = new ItemBuilder(testUserId, "Apples")
                                    .WithNutrition(new NutritionBuilder()
                                                .WithCalories(100))
                                    .WithConsumedTime(new ConsumedTimeBuilder()
                                                .WithTime(_foodsEatenTime))
                                    .Build();

        Item[] eatenFoods = [consumedFood1, consumedFood2];
        _numEatenFoods = eatenFoods.Length;

        _dbContext.AddRange(eatenFoods);
        _dbContext.SaveChanges();
    }
}