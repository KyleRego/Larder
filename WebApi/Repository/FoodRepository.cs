using Microsoft.EntityFrameworkCore;

using Larder.Data;
using Larder.Models;

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

public interface IFoodRepository : IRepositoryBase<Food, FoodSortOptions>
{
    public Task<Food> FindOrCreateBy(string userId, string name);
}

public class FoodRepository(AppDbContext dbContext)
            : RepositoryBase<Food, FoodSortOptions>(dbContext), IFoodRepository
{
    public async Task<Food> FindOrCreateBy(string userId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ApplicationException("ingredient name cannot be null or whitespace");
        
        Food? food = await _dbContext.Foods.FirstOrDefaultAsync(
                f => f.UserId == userId && f.Name == name);

        if (food != null) return food;

        food = new() { Name = name, UserId = userId };
        _dbContext.Foods.Add(food);
        await _dbContext.SaveChangesAsync();

        return food;
    }

    public override async Task<Food?> Get(string id)
    {
        return await _dbContext.Foods.FirstOrDefaultAsync(
                                                    food => food.Id == id);
    }

    public override Task<List<Food>> GetAllForUser(string userId,
                            FoodSortOptions sortBy, string? search)
    {
        var baseQuery = _dbContext.Foods.Where(food => food.UserId == userId);

        var withSearch = (search == null) ? baseQuery : baseQuery.Where(
            food => food.Name.Contains(search)
        );

        switch(sortBy)
        {
            case FoodSortOptions.Name:
                return withSearch.OrderBy(f => f.Name).ToListAsync();
            case FoodSortOptions.Name_Desc:
                return withSearch.OrderByDescending(f => f.Name).ToListAsync();
            case FoodSortOptions.Servings:
                return withSearch.OrderBy(f => f.Servings).ToListAsync();
            case FoodSortOptions.Servings_Desc:
                return withSearch.OrderByDescending(f => f.Servings).ToListAsync();
            case FoodSortOptions.Calories:
                return withSearch.OrderBy(f => f.Calories).ToListAsync();
            case FoodSortOptions.Calories_Desc:
                return withSearch.OrderByDescending(f => f.Calories).ToListAsync();
            case FoodSortOptions.TotalCalories:
                return withSearch.OrderBy(f => f.TotalCalories).ToListAsync();
            case FoodSortOptions.TotalCalories_Desc:
                return withSearch.OrderByDescending(f => f.TotalCalories).ToListAsync();
            case FoodSortOptions.TotalGramsProtein:
                return withSearch.OrderBy(f => f.TotalGramsProtein).ToListAsync();
            case FoodSortOptions.TotalGramsProtein_Desc:
                return withSearch.OrderByDescending(f => f.TotalGramsProtein).ToListAsync();
            default:
                return withSearch.ToListAsync();
        }
    }
}
