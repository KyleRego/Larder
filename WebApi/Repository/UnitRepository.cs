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

public class UnitRepository(AppDbContext dbContext)
                            : RepositoryBase<Unit, UnitSortOptions>(dbContext),
                                                                IUnitRepository
{
    public override async Task<Unit?> Get(string id)
    {
        return await _dbContext.Units
                        .Include(u => u.Conversions)
                        .Include(u => u.TargetConversions)
                        .FirstOrDefaultAsync(
                            unit => unit.Id == id);
    }

    public override async Task<List<Unit>> GetAllForUser(string userId,
                                    UnitSortOptions sortBy, string? search)
    {
        var baseQuery = _dbContext.Units.Where(unit => unit.UserId == userId);

        var baseSearchQuery = (search == null) ? baseQuery
                    : baseQuery.Where(unit => unit.Name.Contains(search));

        switch (sortBy)
        {
            case UnitSortOptions.Name:
                return await baseSearchQuery.OrderBy(u => u.Name).ToListAsync();

            case UnitSortOptions.Name_Desc:
                return await baseSearchQuery.OrderByDescending(u => u.Name).ToListAsync();

            case UnitSortOptions.Type:
                return await baseSearchQuery.OrderBy(u => u.Type).ToListAsync();

            case UnitSortOptions.Type_Desc:
                return await baseSearchQuery.OrderByDescending(u => u.Type).ToListAsync();

            default:
                return await baseSearchQuery.ToListAsync();
        }
    }
}
