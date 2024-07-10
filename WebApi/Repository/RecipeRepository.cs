using Microsoft.EntityFrameworkCore;
using Larder.Data;
using Larder.Models;

namespace Larder.Repository;

public interface IRecipeRepository
{
    public Task<List<Recipe>> GetRecipes();
}

public class RecipeRepository(AppDbContext dbContext) : IRecipeRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<List<Recipe>> GetRecipes()
    {
        return await _dbContext.Recipes.ToListAsync();
    }
}