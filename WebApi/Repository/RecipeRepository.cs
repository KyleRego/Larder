using Microsoft.EntityFrameworkCore;
using Larder.Data;
using Larder.Models;

namespace Larder.Repository;

public interface IRecipeRepository : IRepositoryBase<Recipe>
{

}

public class RecipeRepository(AppDbContext dbContext) : IRecipeRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<List<Recipe>> GetAll()
    {
        return await _dbContext.Recipes.ToListAsync();
    }

    public async Task<Recipe?> Get(string id)
    {
        return await _dbContext.Recipes
                                .Include(r => r.RecipeIngredients)
                                .ThenInclude(ri => ri.Ingredient)
                                .Include(r => r.RecipeIngredients)
                                .ThenInclude(ri => ri.Unit)
                                .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Recipe> Update(Recipe recipe)
    {
        _dbContext.Entry(recipe).State = EntityState.Modified;

        await _dbContext.SaveChangesAsync();

        return recipe;
    }

    public async Task<Recipe> Insert(Recipe newRecipe)
    {
        
        _dbContext.Recipes.Add(newRecipe);

        await _dbContext.SaveChangesAsync();

        return newRecipe;
    }

    public Task Delete(Recipe t)
    {
        throw new NotImplementedException();
    }
}