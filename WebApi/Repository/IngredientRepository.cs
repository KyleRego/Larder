using Larder.Data;
using Larder.Models;
using Microsoft.EntityFrameworkCore;

namespace Larder.Repository;

public interface IIngredientRepository
{
    public Task<List<Ingredient>> GetIngredients();

    public Task<Ingredient?> GetIngredient(string id, bool withMore);

    public Task<Ingredient> InsertIngredient(Ingredient ingredient);

    public Task<Ingredient> Update(Ingredient ingredient);

    public Task<Ingredient> FindOrCreateBy(string name);
}

public class IngredientRepository(AppDbContext dbContext) : IIngredientRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Ingredient> FindOrCreateBy(string name)
    {
        Ingredient? ingredient = _dbContext.Ingredients.FirstOrDefault(ing => ing.Name == name);
        if (ingredient != null) return ingredient;

        ingredient = new() { Name = name };
        _dbContext.Ingredients.Add(ingredient);
        await _dbContext.SaveChangesAsync();

        return ingredient;
    }

    // TODO: Think about design of repository with eager loading not always
    public async Task<Ingredient?> GetIngredient(string id, bool withMore)
    {
        if (withMore == true)
        {
            return await _dbContext.Ingredients
                                .Include(ing => ing.Unit)
                                .Include(ing => ing.RecipeIngredients)
                                .ThenInclude(ri => ri.Recipe)
                                .FirstOrDefaultAsync(ing => ing.Id == id);
        }
        else
        {
            return await _dbContext.Ingredients
                                .Include(ing => ing.Unit)
                                .FirstOrDefaultAsync(ing => ing.Id == id);
        }
    }

    public async Task<List<Ingredient>> GetIngredients()
    {
        return await _dbContext.Ingredients
                                .Include(ing => ing.Unit)
                                .ToListAsync();
    }

    public async Task<Ingredient> InsertIngredient(Ingredient ingredient)
    {
        _dbContext.Ingredients.Add(ingredient);

        await _dbContext.SaveChangesAsync();

        return ingredient;
    }

    public async Task<Ingredient> Update(Ingredient ingredient)
    {
        _dbContext.Entry(ingredient).State = EntityState.Modified;

        await _dbContext.SaveChangesAsync();

        return ingredient;
    }
}