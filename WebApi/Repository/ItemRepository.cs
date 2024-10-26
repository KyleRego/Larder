using Larder.Data;
using Larder.Models;
using Microsoft.EntityFrameworkCore;

namespace Larder.Repository;

public enum ItemSortOptions
{
    AnyOrder,
    Name,
    Name_Desc,
    Description,
    Description_Desc
}

public class ItemRepository(AppDbContext dbContext)
                        : RepositoryBase<Item, ItemSortOptions>(dbContext), IItemRepository
{
    public override async Task<Item?> Get(string id)
    {
        return await _dbContext.Items
                                .Include(item => item.Food)
                                .Include(item => item.Ingredient)
                                .FirstOrDefaultAsync(item => item.Id == id);
    }

    public override async Task<List<Item>> GetAllForUser(string userId,
                                                    ItemSortOptions sortBy,
                                                    string? search)
    {
        var query = _dbContext.Items.Where(item => item.UserId == userId);

        if (search != null)
        {
            query = query.Where(item => item.Name == search);
        }

        switch(sortBy)
        {
            case ItemSortOptions.Name:
                query = query.OrderBy(item => item.Name);
                break;
            case ItemSortOptions.Name_Desc:
                query = query.OrderByDescending(item => item.Name);
                break;
            default:
                break;
        }

        return await query.ToListAsync();
    }
}
