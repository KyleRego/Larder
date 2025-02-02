using Microsoft.EntityFrameworkCore;

using Larder.Data;
using Larder.Models;
using Larder.Models.ItemComponents;
using Larder.Repository.Interface;
using Larder.Models.SortOptions;

namespace Larder.Repository.Impl;

public class FoodRepository(AppDbContext dbContext)
            : RepositoryBase<Item, FoodSortOptions>(dbContext), IFoodRepository
{
    public async Task<Item> FindOrCreateBy(string userId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ApplicationException("ingredient name cannot be null or whitespace");

        Item? foodItem = await _dbContext.Items.FirstOrDefaultAsync(item =>
            item.UserId == userId && item.Name == name && item.Nutrition != null);

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
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsTotalFat:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsTotalFat_Desc:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsSaturatedFat:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsSaturatedFat_Desc:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsTransFat:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsTransFat_Desc:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.MilligramsCholesterol:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.MilligramsCholesterol_Desc:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.MilligramsSodium:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.MilligramsSodium_Desc:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsTotalCarbs:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsTotalCarbs_Desc:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsDietaryFiber:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsDietaryFiber_Desc:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsTotalSugars:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            case FoodSortOptions.GramsTotalSugars_Desc:
                query = query.OrderBy(item => item.Nutrition!.GramsProtein);
                break;
            default:
                break;
        }

        return query.ToListAsync();
    }
}
