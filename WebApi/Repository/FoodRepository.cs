using Microsoft.EntityFrameworkCore;

using Larder.Data;
using Larder.Models;

namespace Larder.Repository;

public enum FoodSortOptions
{
    AnyOrder,
    Name,
    Name_Desc,
    Servings,
    Servings_Desc,
    Calories,
    Calories_Desc
}

public interface IFoodRepository : IRepositoryBase<Food, FoodSortOptions>
{
}

public class FoodRepository(AppDbContext dbContext) : RepositoryBase<Food, FoodSortOptions>(dbContext), IFoodRepository
{
    public override async Task<Food?> Get(string id)
    {
        return await _dbContext.Foods.Include(f => f.Recipe)
                                        .FirstOrDefaultAsync(food => food.Id == id);
    }

    public override Task<List<Food>> GetAll(FoodSortOptions sortBy, string? search)
    {
        var baseQuery = _dbContext.Foods.Include(f => f.Recipe);

        var withSearch = (search == null) ? baseQuery : baseQuery.Where(food => food.Name.Contains(search));

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
            default:
                return withSearch.ToListAsync();
        }
    }
}
