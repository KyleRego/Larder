using Larder.Data;
using Larder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace Larder.Repository;

public enum FoodSortOptions
{
    AnyOrder,
}

public interface IFoodRepository : IRepositoryBase<Food, FoodSortOptions>
{

}

public class FoodRepository(AppDbContext dbContext) : RepositoryBase<Food, FoodSortOptions>(dbContext), IFoodRepository
{
    public override async Task<Food?> Get(string id)
    {
        return await _dbContext.Foods.FirstOrDefaultAsync(food => food.Id == id);
    }

    public override Task<List<Food>> GetAll(FoodSortOptions sortBy, string? search)
    {
        var baseQuery = _dbContext.Foods.Include(food => food.Recipe);

        var withSearch = (search == null) ? baseQuery : baseQuery.Where(food => food.Name.Contains(search));

        return withSearch.ToListAsync();
    }
}
