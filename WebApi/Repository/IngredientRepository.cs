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
        if (string.IsNullOrWhiteSpace(name))
            throw new ApplicationException("ingredient name cannot be null or whitespace");
        
        Ingredient? ingredient = _dbContext.Ingredients.FirstOrDefault(ing => ing.Name == name);
        if (ingredient != null) return ingredient;

        ingredient = new() {
            Name = name,
            Quantity = new() { Amount = 1 }
        };
        _dbContext.Ingredients.Add(ingredient);
        await _dbContext.SaveChangesAsync();

        return ingredient;
    }

    public override async Task<Ingredient?> Get(string id)
    {
        return await _dbContext.Ingredients
                            .Include(ing => ing.Quantity)
                            .Include(ing => ing.RecipeIngredients)
                            .ThenInclude(ri => ri.Recipe)
                            .FirstOrDefaultAsync(ing => ing.Id == id);
    }

    public override async Task<List<Ingredient>> GetAll(IngredientSortOptions sortBy, string? search)
    {
        var baseQuery = _dbContext.Ingredients.Include(ing => ing.Quantity);

        var baseSearchQuery = (search == null) ? baseQuery : baseQuery.Where(ingredient => ingredient.Name.Contains(search));

        switch (sortBy)
        {
            case IngredientSortOptions.Name:
                return await baseSearchQuery.OrderBy(ing => ing.Name).ToListAsync();

            case IngredientSortOptions.Name_Desc:
                return await baseSearchQuery.OrderByDescending(ing => ing.Name).ToListAsync();

            case IngredientSortOptions.Quantity:
                return await baseSearchQuery.OrderBy(ing => ing.Quantity.Amount).ToListAsync();

            case IngredientSortOptions.Quantity_Desc:
                return await baseSearchQuery.OrderByDescending(ing => ing.Quantity.Amount).ToListAsync();

            default:
                return await baseSearchQuery.ToListAsync();
        }
        
    }
}
