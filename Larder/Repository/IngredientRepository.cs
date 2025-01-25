using Microsoft.EntityFrameworkCore;

using Larder.Data;
using Larder.Models;
using Larder.Models.ItemComponents;

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
        
        Item? ingredientItem = _dbContext.Items.FirstOrDefault(item =>
            item.UserId == userId && item.Name == name && item.Ingredient != null);

        if (ingredientItem != null) return ingredientItem;

        ingredientItem = new(userId, name, 1, null);

        QuantityComponent quantityComponent = new()
        {
            Item = ingredientItem,
            Quantity = new() { Amount = 1 }
        };
        ingredientItem.QuantityComp = quantityComponent;

        Ingredient ing = new()
        {
            Item = ingredientItem
        };
        ingredientItem.Ingredient = ing;

        _dbContext.Items.Add(ingredientItem);
        await _dbContext.SaveChangesAsync();

        return ingredientItem;
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
                                    .Include(item => item.QuantityComp)
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
                return await baseSearchQuery.OrderBy(item => item.QuantityComp!.Quantity.Amount).ToListAsync();

            case IngredientSortOptions.Quantity_Desc:
                return await baseSearchQuery.OrderByDescending(item => item.QuantityComp!.Quantity.Amount).ToListAsync();

            default:
                return await baseSearchQuery.ToListAsync();
        }
    }
}
