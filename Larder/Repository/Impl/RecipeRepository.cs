using Microsoft.EntityFrameworkCore;

using Larder.Repository.Impl;
using Larder.Models;
using Larder.Repository.Interface;
using Larder.Models.SortOptions;

namespace Larder.Repository.Impl;

public class RecipeRepository(AppDbContext dbContext)
                : CrudRepositoryBase<Recipe>(dbContext),
                                IRecipeRepository
{
    /// <summary>
    /// Get recipe, eager load recipe ingredients and ingredients
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task<Recipe?> GetOrNull(string userId, string id)
    {
        return await _dbContext.Recipes
                                .Include(r => r.RecipeIngredients)
                                .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
    }

    public async Task<List<Recipe>> GetAll(string userId,
                        RecipeSortOptions sortBy = RecipeSortOptions.AnyOrder, 
                        string? search = null)
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
