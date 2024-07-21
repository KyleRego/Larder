using Microsoft.EntityFrameworkCore;

using Larder.Data;
using Larder.Models;

namespace Larder.Repository;

public enum IngredientSortOptions
{
    AnyOrder,
    Name,
    Name_Desc,
    Quantity,
    Quantity_Desc
}

public interface IIngredientRepository : IRepositoryBase<Ingredient, IngredientSortOptions>
{
    public Task<Ingredient> FindOrCreateBy(string name);
}

public class IngredientRepository(AppDbContext dbContext) : RepositoryBase<Ingredient, IngredientSortOptions>(dbContext), IIngredientRepository
{
    public async Task<Ingredient> FindOrCreateBy(string name)
    {
        Ingredient? ingredient = _dbContext.Ingredients.FirstOrDefault(ing => ing.Name == name);
        if (ingredient != null) return ingredient;

        ingredient = new() { Name = name };
        _dbContext.Ingredients.Add(ingredient);
        await _dbContext.SaveChangesAsync();

        return ingredient;
    }

    public override async Task<Ingredient?> Get(string id)
    {
        return await _dbContext.Ingredients
                            .Include(ing => ing.Unit)
                            .Include(ing => ing.RecipeIngredients)
                            .ThenInclude(ri => ri.Recipe)
                            .FirstOrDefaultAsync(ing => ing.Id == id);
    }

    public override async Task<List<Ingredient>> GetAll(IngredientSortOptions sortBy)
    {
        var baseQuery = _dbContext.Ingredients.Include(ingredient => ingredient.Unit);

        switch (sortBy)
        {
            case IngredientSortOptions.Name:
                return await baseQuery.OrderBy(ing => ing.Name).ToListAsync();

            case IngredientSortOptions.Name_Desc:
                return await baseQuery.OrderByDescending(ing => ing.Name).ToListAsync();

            case IngredientSortOptions.Quantity:
                return await baseQuery.OrderBy(ing => ing.Quantity).ToListAsync();

            case IngredientSortOptions.Quantity_Desc:
                return await baseQuery.OrderByDescending(ing => ing.Quantity).ToListAsync();

            default:
                return await baseQuery.ToListAsync();
        }
        
    }
}