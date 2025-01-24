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

public class RecipeRepository(AppDbContext dbContext)
                : RepositoryBase<Recipe, RecipeSortOptions>(dbContext),
                                                        IRecipeRepository
{
    /// <summary>
    /// Get recipe, eager load recipe ingredients and ingredients
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task<Recipe?> Get(string userId, string id)
    {
        return await _dbContext.Recipes
                                .Include(r => r.RecipeIngredients)
                                .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
    }

    public override async Task<List<Recipe>> GetAll(string userId,
                                RecipeSortOptions sortBy, string? search)
    {
        var baseQuery = _dbContext.Recipes.Where(recipe => recipe.UserId == userId);

        var baseSearchQuery = (search == null) ? baseQuery : baseQuery.Where(
                                            recipe => recipe.Name.Contains(search));

        switch (sortBy)
        {
            case RecipeSortOptions.Name:
                return await baseSearchQuery.OrderBy(rec => rec.Name).ToListAsync();

            case RecipeSortOptions.Name_Desc:
                return await baseSearchQuery.OrderByDescending(rec => rec.Name).ToListAsync();

            default:
                return await baseSearchQuery.ToListAsync();
        }
    }
}
