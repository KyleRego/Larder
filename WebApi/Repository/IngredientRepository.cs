using Larder.Data;
using Larder.Models;
using Microsoft.EntityFrameworkCore;

namespace Larder.Repository;

public interface IIngredientRepository : IRepositoryBase<Ingredient>
{
    public Task<Ingredient> FindOrCreateBy(string name);
}

public class IngredientRepository(AppDbContext dbContext) : IIngredientRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Delete(Ingredient t)
    {
        _dbContext.Entry(t).State = EntityState.Deleted;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<Ingredient> FindOrCreateBy(string name)
    {
        Ingredient? ingredient = _dbContext.Ingredients.FirstOrDefault(ing => ing.Name == name);
        if (ingredient != null) return ingredient;

        ingredient = new() { Name = name };
        _dbContext.Ingredients.Add(ingredient);
        await _dbContext.SaveChangesAsync();

        return ingredient;
    }

    public async Task<Ingredient?> Get(string id)
    {
        return await _dbContext.Ingredients
                            .Include(ing => ing.Unit)
                            .Include(ing => ing.RecipeIngredients)
                            .ThenInclude(ri => ri.Recipe)
                            .FirstOrDefaultAsync(ing => ing.Id == id);
    }

    public async Task<List<Ingredient>> GetAll()
    {
        return await _dbContext.Ingredients
                                .Include(ing => ing.Unit)
                                .ToListAsync();
    }

    public async Task<Ingredient> Insert(Ingredient ingredient)
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