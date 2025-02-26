using Microsoft.EntityFrameworkCore;
using Larder.Data;
using Larder.Models;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Repository.Impl;

public class UnitRepository(AppDbContext dbContext)
                            : CrudRepositoryBase<Unit>(dbContext),
                                IUnitRepository
{
    public override async Task<Unit?> Get(string userId, string id)
    {
        return await _dbContext.Units
                        .Include(u => u.Conversions)
                        .Include(u => u.TargetConversions)
                        .FirstOrDefaultAsync(
                            unit => unit.Id == id && unit.UserId == userId);
    }

    public async Task<List<Unit>> GetAll(string userId,
                                    UnitSortOptions sortBy = UnitSortOptions.AnyOrder,
                                    string? search = null)
    {
        var searchQuery = _dbContext.Units.Where(unit => unit.UserId == userId);

        searchQuery = (search == null) ? searchQuery
                    : searchQuery.Where(unit => unit.Name.Contains(search));

        switch (sortBy)
        {
            case UnitSortOptions.Name:
                searchQuery = searchQuery.OrderBy(u => u.Name);
                break;

            case UnitSortOptions.Name_Desc:
                searchQuery = searchQuery.OrderByDescending(u => u.Name);
                break;

            case UnitSortOptions.Type:
                searchQuery = searchQuery.OrderBy(u => u.Type);
                break;

            case UnitSortOptions.Type_Desc:
                searchQuery = searchQuery.OrderByDescending(u => u.Type);
                break;
        }

        return await searchQuery.ToListAsync();
    }
}
