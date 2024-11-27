using Microsoft.EntityFrameworkCore;

using Larder.Data;
using Larder.Models;
using Larder.Models.ItemComponent;

namespace Larder.Repository;

public enum FoodSortOptions
{
    AnyOrder,
    Name, Name_Desc,
    Servings, Servings_Desc,
    Calories, Calories_Desc,
    TotalCalories, TotalCalories_Desc,
    TotalGramsProtein, TotalGramsProtein_Desc
}

public class FoodRepository(AppDbContext dbContext)
            : RepositoryBase<Item, FoodSortOptions>(dbContext), IFoodRepository
{
    public async Task<Item> FindOrCreateBy(string userId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ApplicationException("ingredient name cannot be null or whitespace");

        Item? foodItem = await _dbContext.Items.FirstOrDefaultAsync(item =>
            item.UserId == userId && item.Name == name && item.Food != null);

        if (foodItem != null) return foodItem;

        foodItem = new(userId, name, null);
        Food food = new() { Item = foodItem };
        foodItem.Food = food;

        _dbContext.Items.Add(foodItem);
        await _dbContext.SaveChangesAsync();

        return foodItem;
    }

    public override async Task<Item?> Get(string userId, string id)
    {
        return await _dbContext.Items.FirstOrDefaultAsync(item =>
            item.Id == id && item.UserId == userId && item.Food != null);
    }

    public override Task<List<Item>> GetAll(string userId,
                            FoodSortOptions sortBy, string? search)
    {
        var baseQuery = _dbContext.Items
                                .Include(item => item.Food)
                                .Where(item =>
            item.UserId == userId && item.Food != null);

        var withSearch = (search == null) ? baseQuery : baseQuery.Where(
            food => food.Name.Contains(search)
        );

        switch(sortBy)
        {
            case FoodSortOptions.Name:
                return withSearch.OrderBy(item => item.Name).ToListAsync();
            case FoodSortOptions.Name_Desc:
                return withSearch.OrderByDescending(item => item.Name).ToListAsync();
            case FoodSortOptions.Servings:
                return withSearch.OrderBy(item => item.Food!.Servings).ToListAsync();
            case FoodSortOptions.Servings_Desc:
                return withSearch.OrderByDescending(item => item.Food!.Servings).ToListAsync();
            case FoodSortOptions.Calories:
                return withSearch.OrderBy(item => item.Food!.Calories).ToListAsync();
            case FoodSortOptions.Calories_Desc:
                return withSearch.OrderByDescending(item => item.Food!.Calories).ToListAsync();
            case FoodSortOptions.TotalCalories:
                return withSearch.OrderBy(item => item.Food!.TotalCalories).ToListAsync();
            case FoodSortOptions.TotalCalories_Desc:
                return withSearch.OrderByDescending(item => item.Food!.TotalCalories).ToListAsync();
            case FoodSortOptions.TotalGramsProtein:
                return withSearch.OrderBy(item => item.Food!.TotalGramsProtein).ToListAsync();
            case FoodSortOptions.TotalGramsProtein_Desc:
                return withSearch.OrderByDescending(item => item.Food!.TotalGramsProtein).ToListAsync();
            default:
                return withSearch.ToListAsync();
        }
    }
}
