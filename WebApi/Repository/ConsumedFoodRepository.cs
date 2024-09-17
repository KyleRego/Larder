using Larder.Data;
using Larder.Models;
using Microsoft.EntityFrameworkCore;

namespace Larder.Repository;

public enum ConsumedFoodSortOptions
{
    AnyOrder
}

public interface IConsumedFoodRepository :
                        IRepositoryBase<ConsumedFood, ConsumedFoodSortOptions>
{
    public Task<List<ConsumedFood>> GetConsumedFoodsPastWeek(string userId);
}

public class ConsumedFoodRepository(AppDbContext dbContext) :
            RepositoryBase<ConsumedFood, ConsumedFoodSortOptions>(dbContext),
                                                        IConsumedFoodRepository
                                                    
{
    public async Task<List<ConsumedFood>>
                                        GetConsumedFoodsPastWeek(string userId)
    {
        DateOnly sevenDaysAgo = DateOnly.FromDateTime(
                                            DateTime.Today.AddDays(-7));

        return await _dbContext.ConsumedFoods.Where(
            f => f.UserId == userId && f.DateConsumed > sevenDaysAgo
        ).ToListAsync();
    }

    public async override Task<ConsumedFood?> Get(string id)
    {
        return await _dbContext.ConsumedFoods
                        .FirstOrDefaultAsync(cf => cf.Id == id);
    }

    public override Task<List<ConsumedFood>> GetAllForUser(string userId,
                                ConsumedFoodSortOptions sortBy, string? search)
    {
        throw new NotImplementedException();
    }
}
