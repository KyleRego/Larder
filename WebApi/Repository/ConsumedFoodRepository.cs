using Larder.Data;
using Larder.Models;
using Microsoft.EntityFrameworkCore;

namespace Larder.Repository;

public enum ConsumedFoodSortOptions
{
    AnyOrder
}

public interface IConsumedFoodRepository : IRepositoryBase<ConsumedFood, ConsumedFoodSortOptions>
{
    public Task<List<ConsumedFood>> GetConsumedFoodsPastWeek();
}

public class ConsumedFoodRepository(AppDbContext dbContext) : RepositoryBase<ConsumedFood, ConsumedFoodSortOptions>(dbContext),
                                                                 IConsumedFoodRepository
                                                    
{
    public async Task<List<ConsumedFood>> GetConsumedFoodsPastWeek()
    {
        DateTime sevenDaysAgo = DateTime.Today.AddDays(-7);

        return await _dbContext.ConsumedFoods.Where(f => f.DateTimeConsumed > sevenDaysAgo).ToListAsync();
    }

    public override Task<ConsumedFood?> Get(string id)
    {
        throw new NotImplementedException();
    }

    public override Task<List<ConsumedFood>> GetAll(ConsumedFoodSortOptions sortBy, string? search)
    {
        throw new NotImplementedException();
    }
}
