using Larder.Data;
using Larder.Models;
using Microsoft.EntityFrameworkCore;

namespace Larder.Repository;

public enum ItemSortOptions
{
    AnyOrder,
    Name,
    Name_Desc,
    Amount,
    Amount_Desc,
    Description,
    Description_Desc
}

public class ItemRepository(AppDbContext dbContext)
            : RepositoryBase<Item, ItemSortOptions>(dbContext), IItemRepository
{
    public override async Task<Item?> Get(string userId, string id)
    {
        return await _dbContext.Items
                                .Include(item => item.Nutrition)
                                .Include(item => item.Ingredient)
                                .FirstOrDefaultAsync(
                            item => item.Id == id && item.UserId == userId);
    }

    public override async Task<List<Item>> GetAll(string userId,
                                                    ItemSortOptions sortBy,
                                                    string? search)
    {
        var query = _dbContext.Items.Where(item => item.UserId == userId);

        if (search != null)
        {
            query = query.Where(item => item.Name.Contains(search));
        }

        switch(sortBy)
        {
            case ItemSortOptions.Name:
                query = query.OrderBy(item => item.Name);
                break;
            case ItemSortOptions.Name_Desc:
                query = query.OrderByDescending(item => item.Name);
                break;
            case ItemSortOptions.Amount:
                query = query.OrderBy(item => item.Quantity.Amount);
                break;
            case ItemSortOptions.Amount_Desc:
                query = query.OrderByDescending(item => item.Quantity.Amount);
                break;
            case ItemSortOptions.Description:
                query = query.OrderBy(item => item.Description);
                break;
            case ItemSortOptions.Description_Desc:
                query = query.OrderByDescending(item => item.Description);
                break;
            default:
                break;
        }

        return await query.ToListAsync();
    }
}
