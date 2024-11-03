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

public class IngredientRepository(AppDbContext dbContext)
    : RepositoryBase<Item, IngredientSortOptions>(dbContext),
                                                IIngredientRepository
{
    public async Task<Item> FindOrCreateBy(string userId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ApplicationException("ingredient name cannot be null or whitespace");
        
        Item? ingItem = _dbContext.Items.FirstOrDefault(item =>
            item.UserId == userId && item.Name == name && item.Ingredient != null);

        if (ingItem != null) return ingItem;

        ingItem = new(userId, name, null) {
            Amount = 1
        };

        Ingredient ing = new()
        {
            Item = ingItem,
            Quantity = new() { Amount = 1 }
        };
        ingItem.Ingredient = ing;

        _dbContext.Items.Add(ingItem);
        await _dbContext.SaveChangesAsync();

        return ingItem;
    }

    public override async Task<Item?> Get(string userId, string id)
    {
        return await _dbContext.Items
                            .Include(item => item.Ingredient)
                            .ThenInclude(ing => ing!.Recipes)
                            .ThenInclude(ing => ing!.RecipeIngredients)
                            .ThenInclude(ing => ing!.Quantity)
                            .FirstOrDefaultAsync(
            item => item.Id == id && item.UserId == userId
                                    && item.Ingredient != null);
    }

    public override async Task<List<Item>> GetAll(string userId,
                                                IngredientSortOptions sortBy,
                                                                string? search)
    {
        var baseQuery = _dbContext.Items
                                    .Include(item => item.Ingredient)
                                    .ThenInclude(ing => ing!.Quantity)
                                    .Where(item => 
            item.UserId == userId && item.Ingredient != null);

        var baseSearchQuery = (search == null) ? baseQuery
            : baseQuery.Where(ingredient => ingredient.Name.Contains(search));

        switch (sortBy)
        {
            case IngredientSortOptions.Name:
                return await baseSearchQuery.OrderBy(item => item.Name).ToListAsync();

            case IngredientSortOptions.Name_Desc:
                return await baseSearchQuery.OrderByDescending(item => item.Name).ToListAsync();

            case IngredientSortOptions.Quantity:
                return await baseSearchQuery.OrderBy(item => item.Ingredient!.Quantity.Amount).ToListAsync();

            case IngredientSortOptions.Quantity_Desc:
                return await baseSearchQuery.OrderByDescending(item => item.Ingredient!.Quantity.Amount).ToListAsync();

            default:
                return await baseSearchQuery.ToListAsync();
        }
    }
}
