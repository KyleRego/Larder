using Larder.Repository.Impl;
using Larder.Models;
using Microsoft.EntityFrameworkCore;

namespace Larder.Repository.Impl;

using Larder.Models.Builders;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

public class ItemRepository(AppDbContext dbContext)
            : CrudRepositoryBase<Item>(dbContext), IItemRepository
{
    public async Task<Item> FindOrCreate(string userId, string name)
    {
        Item? existing = await Get(userId, name);
        if (existing != null) return existing;

        Item newItem = new ItemBuilder(userId, name).Build();
        await Insert(newItem);
        return newItem;
    }

    public override async Task<Item?> Get(string userId, string id)
    {
        return await _dbContext.Items
                                .Include(item => item.Nutrition)
                                .Include(item => item.Quantity)
                                .Include(item => item.Recipes)
                                .Include(item => item.Container)
                                .ThenInclude(container => container!.Items)
                                .FirstOrDefaultAsync(
                            item => item.Id == id && item.UserId == userId);
    }

    public async Task<List<Item>> GetAll(string userId,
                            ItemSortOptions sortBy = ItemSortOptions.AnyOrder,
                            string? search = null)
    {
        var query = _dbContext.Items.Where(item =>
            item.UserId == userId && item.ConsumedTime == null);

        if (search != null)
        {
            query = query.Where(item => item.Name.Contains(search));
        }

        switch(sortBy)
        {
            case ItemSortOptions.Name:
                query = query.OrderBy(item => item.Name);
                break;
            case ItemSortOptions.Name_Desc:
                query = query.OrderByDescending(item => item.Name);
                break;
            case ItemSortOptions.Amount:
                query = query.OrderBy(item => item.Quantity.Amount);
                break;
            case ItemSortOptions.Amount_Desc:
                query = query.OrderByDescending(item => item.Quantity.Amount);
                break;
            case ItemSortOptions.Description:
                query = query.OrderBy(item => item.Description);
                break;
            case ItemSortOptions.Description_Desc:
                query = query.OrderByDescending(item => item.Description);
                break;
            default:
                break;
        }

        return await query.ToListAsync();
    }

    public Task<List<Item>> GetAllContainers(string userId)
    {
        return _dbContext.Items.Where(
            item => item.UserId == userId && item.Container != null)
                                .ToListAsync();
    }

    public Task<List<Item>> GetAllFoods(string userId,
            FoodSortOptions sortBy = FoodSortOptions.AnyOrder, string? search = null)
    {
        var query = _dbContext.Items
                                .Include(item => item.Nutrition)
                                .Where(item =>
            item.UserId == userId && item.Nutrition != null
            && item.ConsumedTime == null);

        query = (search == null) ? query : query.Where(
            food => food.Name.Contains(search)
        );

        switch(sortBy)
        {
            case FoodSortOptions.Name:
                query = query.OrderBy(item => item.Name);
                break;
            case FoodSortOptions.Name_Desc:
                query = query.OrderByDescending(item => item.Name);
                break;
            case FoodSortOptions.Quantity:
                query = query.OrderBy(item => item.Quantity.Amount);
                break;
            case FoodSortOptions.Quantity_Desc:
                query = query.OrderByDescending(item => item.Quantity.Amount);
                break;
            case FoodSortOptions.ServingSize:
                query = query.OrderBy(item => item.Nutrition!.ServingSize.Amount);
                break;
            case FoodSortOptions.ServingSize_Desc:
                query = query.OrderByDescending(item => item.Nutrition!.ServingSize.Amount);
                break;
            case FoodSortOptions.Calories:
                query = query.OrderBy(item => item.Nutrition!.Calories);
                break;
            case FoodSortOptions.Calories_Desc:
                query = query.OrderByDescending(item => item.Nutrition!.Calories);
                break;
            case FoodSortOptions.GramsProtein:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsProtein_Desc:
                query = query.OrderByDescending(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsTotalFat:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsTotalFat_Desc:
                query = query.OrderByDescending(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsSaturatedFat:
                query = query.OrderBy(item => item.Nutrition!.GramsSaturatedFat);
                break;
            case FoodSortOptions.GramsSaturatedFat_Desc:
                query = query.OrderByDescending(item => item.Nutrition!.GramsSaturatedFat);
                break;
            case FoodSortOptions.GramsTransFat:
                query = query.OrderBy(item => item.Nutrition!.GramsTransFat);
                break;
            case FoodSortOptions.GramsTransFat_Desc:
                query = query.OrderByDescending(item => item.Nutrition!.GramsTransFat);
                break;
            case FoodSortOptions.MilligramsCholesterol:
                query = query.OrderBy(item => item.Nutrition!.MilligramsCholesterol);
                break;
            case FoodSortOptions.MilligramsCholesterol_Desc:
                query = query.OrderByDescending(item => item.Nutrition!.MilligramsCholesterol);
                break;
            case FoodSortOptions.MilligramsSodium:
                query = query.OrderBy(item => item.Nutrition!.MilligramsSodium);
                break;
            case FoodSortOptions.MilligramsSodium_Desc:
                query = query.OrderByDescending(item => item.Nutrition!.MilligramsSodium);
                break;
            case FoodSortOptions.GramsTotalCarbs:
                query = query.OrderBy(item => item.Nutrition!.GramsTotalCarbs);
                break;
            case FoodSortOptions.GramsTotalCarbs_Desc:
                query = query.OrderByDescending(item => item.Nutrition!.GramsTotalCarbs);
                break;
            case FoodSortOptions.GramsDietaryFiber:
                query = query.OrderBy(item => item.Nutrition!.GramsDietaryFiber);
                break;
            case FoodSortOptions.GramsDietaryFiber_Desc:
                query = query.OrderByDescending(item => item.Nutrition!.GramsDietaryFiber);
                break;
            case FoodSortOptions.GramsTotalSugars:
                query = query.OrderBy(item => item.Nutrition!.GramsTotalSugars);
                break;
            case FoodSortOptions.GramsTotalSugars_Desc:
                query = query.OrderByDescending(item => item.Nutrition!.GramsTotalSugars);
                break;
            default:
                break;
        }

        return query.ToListAsync();
    }

    public async Task<List<Item>> GetAllIngredients(string userId,
                IngredientSortOptions sortBy = IngredientSortOptions.AnyOrder,
                                string? search = null)
    {
        var searchQuery = _dbContext.Items
                                    .Include(item => item.Quantity)
                                    .Include(item => item.Recipes)
                                    .Where(item => 
            item.UserId == userId && item.RecipeIngredients.Count != 0);

        searchQuery = (search == null) ? searchQuery
            : searchQuery.Where(ingredient => ingredient.Name.Contains(search));

        switch (sortBy)
        {
            case IngredientSortOptions.Name:
                searchQuery = searchQuery.OrderBy(item => item.Name);
                break;
            case IngredientSortOptions.Name_Desc:
                searchQuery = searchQuery.OrderByDescending(item => item.Name);
                break;
            case IngredientSortOptions.Quantity:
                searchQuery = searchQuery.OrderBy(item => item.Quantity.Amount);
                break;
            case IngredientSortOptions.Quantity_Desc:
                searchQuery = searchQuery.OrderByDescending(item => item.Quantity.Amount);
                break;
        }

        return await searchQuery.ToListAsync();
    }

    public async Task<List<Item>> GetConsumedFoods(string userId, DateTime day)
    {
        var startOfDay = day.Date;
        var endOfDay = startOfDay.AddDays(1);

        // TODO: This needs to include the consumed food Nutrition
        List<Item> consumedFoods = await _dbContext.Items
                        .Include(item => item.ConsumedTime)
                        .Include(item => item.Nutrition)
                        .Where(item => item.UserId == userId
                            && item.ConsumedTime != null).ToListAsync();

        // FIXME: This comparison needs to be pushed into the database to do
        return [.. consumedFoods.Where(item =>
            item.ConsumedTime!.ConsumedAt >= startOfDay
            && item.ConsumedTime.ConsumedAt < endOfDay)];
    }
}
