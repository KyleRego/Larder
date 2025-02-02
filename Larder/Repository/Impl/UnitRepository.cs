using Microsoft.EntityFrameworkCore;
using Larder.Data;
using Larder.Models;

namespace Larder.Repository.Impl;

using System.Linq;
using Larder.Repository.Interface;

public enum UnitSortOptions
{
    AnyOrder,
    Name,
    Name_Desc,
    Type,
    Type_Desc
}

public class UnitRepository(AppDbContext dbContext)
                            : RepositoryBase<Unit, UnitSortOptions>(dbContext),
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

    public override async Task<List<Unit>> GetAll(string userId,
                                    UnitSortOptions sortBy, string? search)
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

    public async Task<List<Unit>> InsertAll(List<Unit> units)
    {
        _dbContext.Units.AddRange(units);
        await _dbContext.SaveChangesAsync();

        string userId = units.First().UserId;
        List<string> unitNames = [.. units.Select(u => u.Name)];

        List<Unit> savedUnits =
            [.. _dbContext.Units.Where(
                u => u.UserId == userId && unitNames.Contains(u.Name))];
        
        return savedUnits;
    }
}
