using Larder.Data;
using Larder.Models;
using Microsoft.EntityFrameworkCore;

namespace Larder.Repository;

public interface IIngredientRepository
{
    public Task<List<Ingredient>> GetIngredients();

    public Task<Ingredient> FindOrCreateBy(string ingredientName);
}

public class IngredientRepository(AppDbContext dbContext) : IIngredientRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Ingredient> FindOrCreateBy(string ingredientName)
    {
        Ingredient? ingredient = _dbContext.Ingredients.FirstOrDefault(ing => ing.Name == ingredientName);
        if (ingredient != null) return ingredient;

        ingredient = new() { Name = ingredientName };
        _dbContext.Ingredients.Add(ingredient);
        await _dbContext.SaveChangesAsync();

        return ingredient;
    }

    public async Task<List<Ingredient>> GetIngredients()
    {
        return await _dbContext.Ingredients.ToListAsync();
    }
}