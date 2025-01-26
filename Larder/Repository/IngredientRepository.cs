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
        
        Item? item = _dbContext.Items.FirstOrDefault(item =>
            item.UserId == userId && item.Name == name && item.Ingredient != null);

        if (item != null) return item;

        item = new(userId, name, null)
        {
            Quantity = new() { Amount = 1 }
        };

        Ingredient ing = new()
        {
            Item = item
        };
        item.Ingredient = ing;

        _dbContext.Items.Add(item);
        await _dbContext.SaveChangesAsync();

        return item;
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
        var searchQuery = _dbContext.Items
                                    .Include(item => item.Ingredient)
                                    .Include(item => item.Quantity)
                                    .Where(item => 
            item.UserId == userId && item.Ingredient != null);

        searchQuery = (search == null) ? searchQuery
            : searchQuery.Where(ingredient => ingredient.Name.Contains(search));

        switch (sortBy)
        {
            case IngredientSortOptions.Name:
                searchQuery = searchQuery.OrderBy(item => item.Name);
                break;
            case IngredientSortOptions.Name_Desc:
                searchQuery = searchQuery.OrderByDescending(item => item.Name);
                break;
            case IngredientSortOptions.Quantity:
                searchQuery = searchQuery.OrderBy(item => item.Quantity.Amount);
                break;
            case IngredientSortOptions.Quantity_Desc:
                searchQuery = searchQuery.OrderByDescending(item => item.Quantity.Amount);
                break;
        }

        return await searchQuery.ToListAsync();
    }
}
