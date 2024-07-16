using Microsoft.EntityFrameworkCore;
using Larder.Data;
using Larder.Models;

namespace Larder.Repository;

public interface IRecipeRepository
{
    public Task<List<Recipe>> GetRecipes();

    public Task<Recipe?> GetRecipe(string recipeId);

    public Task<Recipe> UpdateRecipe(Recipe recipe);
}

public class RecipeRepository(AppDbContext dbContext) : IRecipeRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<List<Recipe>> GetRecipes()
    {
        return await _dbContext.Recipes.ToListAsync();
    }

    public async Task<Recipe?> GetRecipe(string recipeId)
    {
        return await _dbContext.Recipes
                                .Include(r => r.RecipeIngredients)
                                .ThenInclude(ri => ri.Ingredient)
                                .Include(r => r.RecipeIngredients)
                                .ThenInclude(ri => ri.Unit)
                                .FirstOrDefaultAsync(r => r.Id == recipeId);
    }

    public async Task<Recipe> UpdateRecipe(Recipe recipe)
    {
        _dbContext.Entry(recipe).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return recipe;
    }
}