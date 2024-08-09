using Microsoft.EntityFrameworkCore;

using Larder.Data;
using Larder.Models;

namespace Larder.Repository;

public enum RecipeSortOptions
{
    AnyOrder,
    Name,
    Name_Desc
}

public interface IRecipeRepository : IRepositoryBase<Recipe, RecipeSortOptions>
{

}

public class RecipeRepository(AppDbContext dbContext) : RepositoryBase<Recipe, RecipeSortOptions>(dbContext), IRecipeRepository
{
    public override async Task<Recipe?> Get(string id)
    {
        return await _dbContext.Recipes
                                .Include(r => r.Food)
                                .Include(r => r.RecipeIngredients)
                                .ThenInclude(ri => ri.Ingredient)
                                .Include(r => r.RecipeIngredients)
                                .FirstOrDefaultAsync(r => r.Id == id);
    }

    public override async Task<List<Recipe>> GetAll(RecipeSortOptions sortBy, string? search)
    {
        var baseQuery = _dbContext.Recipes;

        switch (sortBy)
        {
            case RecipeSortOptions.Name:
                return await baseQuery.OrderBy(rec => rec.Name).ToListAsync();

            case RecipeSortOptions.Name_Desc:
                return await baseQuery.OrderByDescending(rec => rec.Name).ToListAsync();

            default:
                return await baseQuery.ToListAsync();
        }
    }
}
