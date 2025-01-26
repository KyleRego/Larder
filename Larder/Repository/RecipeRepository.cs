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
        var searchQuery = _dbContext.Recipes.Where(recipe => recipe.UserId == userId);

        searchQuery = (search == null) ? searchQuery : searchQuery.Where(
                                    recipe => recipe.Name.Contains(search));

        switch (sortBy)
        {
            case RecipeSortOptions.Name:
                searchQuery = searchQuery.OrderBy(rec => rec.Name);
                break;

            case RecipeSortOptions.Name_Desc:
                searchQuery = searchQuery.OrderByDescending(rec => rec.Name);
                break;
        }

        return await searchQuery.ToListAsync();
    }
}
