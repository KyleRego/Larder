using Microsoft.EntityFrameworkCore;
using Larder.Data;
using Larder.Models;

namespace Larder.Repository;

public enum UnitSortOptions
{
    AnyOrder,
    Name,
    Name_Desc,
    Type,
    Type_Desc
}

public interface IUnitRepository : IRepositoryBase<Unit, UnitSortOptions>
{
    
}

public class UnitRepository(AppDbContext dbContext) : RepositoryBase<Unit, UnitSortOptions>(dbContext), IUnitRepository
{
    public override async Task<Unit?> Get(string id)
    {
        return await _dbContext.Units
                        .Include(u => u.Conversions)
                        .Include(u => u.TargetConversions)
                        .FirstOrDefaultAsync(unit => unit.Id == id);
    }

    public override async Task<List<Unit>> GetAll(UnitSortOptions sortBy, string? search)
    {
        var baseQuery = _dbContext.Units;

        switch (sortBy)
        {
            case UnitSortOptions.Name:
                return await baseQuery.OrderBy(u => u.Name).ToListAsync();

            case UnitSortOptions.Name_Desc:
                return await baseQuery.OrderByDescending(u => u.Name).ToListAsync();

            case UnitSortOptions.Type:
                return await baseQuery.OrderBy(u => u.Type).ToListAsync();

            case UnitSortOptions.Type_Desc:
                return await baseQuery.OrderByDescending(u => u.Type).ToListAsync();

            default:
                return await baseQuery.ToListAsync();
        }
    }
}
