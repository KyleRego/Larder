using Microsoft.EntityFrameworkCore;

using Larder.Data;
using Larder.Models;
using Larder.Models.ItemComponents;
using Larder.Repository.Interface;
using Larder.Models.SortOptions;
using Larder.Dtos;

namespace Larder.Repository.Impl;

public class FoodRepository(AppDbContext dbContext)
            : RepositoryBase<Item, FoodSortOptions>(dbContext), IFoodRepository
{
    public async Task<List<Item>> GetConsumedFoods(string userId, DateTime day)
    {
        var startOfDay = day.Date;
        var endOfDay = startOfDay.AddDays(1);

        // TODO: This needs to include the consumed food Nutrition
        List<Item> consumedFoods = await _dbContext.Items
                        .Include(item => item.ConsumedTime)
                        .Where(item => item.UserId == userId
                            && item.ConsumedTime != null).ToListAsync();

        // FIXME: This comparison needs to be pushed into the database to do
        return [.. consumedFoods.Where(item =>
            item.ConsumedTime!.ConsumedAt >= startOfDay
            && item.ConsumedTime.ConsumedAt < endOfDay)];
    }

    public async Task<Item> FindOrCreateBy(string userId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ApplicationException("Name cannot be null or whitespace");

        Item? foodItem = await _dbContext.Items.FirstOrDefaultAsync(item =>
            item.UserId == userId && item.Name == name
                            && item.Nutrition != null);

        if (foodItem != null) return foodItem;

        foodItem = new(userId, name, null)
        {
            Quantity = new() { Amount = 1 }
        };
        Nutrition food = new()
        {
            Item = foodItem,
        };
        foodItem.Nutrition = food;

        _dbContext.Items.Add(foodItem);
        await _dbContext.SaveChangesAsync();

        return foodItem;
    }

    public override async Task<Item?> Get(string userId, string id)
    {
        return await _dbContext.Items.Include(item => item.Nutrition)
                                        .Include(item => item.Quantity)
                                        .FirstOrDefaultAsync(item =>
            item.Id == id && item.UserId == userId && item.Nutrition != null);
    }

    public override Task<List<Item>> GetAll(string userId,
                            FoodSortOptions sortBy, string? search)
    {
        var query = _dbContext.Items
                                .Include(item => item.Nutrition)
                                .Where(item =>
            item.UserId == userId && item.Nutrition != null);

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
}
