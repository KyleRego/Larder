using Microsoft.EntityFrameworkCore;

using Larder.Data;
using Larder.Models;
using Larder.Models.ItemComponents;
using Larder.Repository.Interface;
using Larder.Models.SortOptions;

namespace Larder.Repository.Impl;

public class IngredientRepository(AppDbContext dbContext)
            : ItemRepository(dbContext), IIngredientRepository
{
    public override async Task<Item?> Get(string userId, string id)
    {
        return await _dbContext.Items
                            .Include(item => item.Ingredient)
                            .ThenInclude(ing => ing!.Recipes)
                            .ThenInclude(ing => ing!.RecipeIngredients)
                            .ThenInclude(ing => ing!.DefaultQuantity)
                            .FirstOrDefaultAsync(
            item => item.Id == id && item.UserId == userId
                                    && item.Ingredient != null);
    }

    public async Task<List<Item>> GetAll(string userId,
                IngredientSortOptions sortBy = IngredientSortOptions.AnyOrder,
                                string? search = null)
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
