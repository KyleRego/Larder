using Microsoft.EntityFrameworkCore;

using Larder.Data;
using Larder.Models;

namespace Larder.Repository;

public enum FoodSortOptions
{
    AnyOrder,
    Name,
    Name_Desc,
    Quantity,
    Quantity_Desc
}

public interface IFoodRepository : IRepositoryBase<Food, FoodSortOptions>
{
}

public class FoodRepository(AppDbContext dbContext) : RepositoryBase<Food, FoodSortOptions>(dbContext), IFoodRepository
{
    public override async Task<Food?> Get(string id)
    {
        return await _dbContext.Foods.Include(f => f.Recipe)
                                        .Include(f => f.Protein!.Unit)
                                        .Include(f => f.TotalFat!.Unit)
                                        .Include(f => f.SaturatedFat!.Unit)
                                        .Include(f => f.TransFat!.Unit)
                                        .Include(f => f.Cholesterol!.Unit)
                                        .Include(f => f.Sodium!.Unit)
                                        .Include(f => f.TotalCarbs!.Unit)
                                        .Include(f => f.DietaryFiber!.Unit)
                                        .Include(f => f.TotalSugars!.Unit)
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
            case FoodSortOptions.Quantity:
                return withSearch.OrderBy(f => f.Quantity).ToListAsync();
            case FoodSortOptions.Quantity_Desc:
                return withSearch.OrderByDescending(f => f.Quantity).ToListAsync();
            default:
                return withSearch.ToListAsync();
        }
    }
}
