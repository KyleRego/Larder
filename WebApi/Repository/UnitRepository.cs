using Microsoft.EntityFrameworkCore;
using Larder.Data;
using Larder.Models;

namespace Larder.Repository;

public interface IUnitRepository
{
    public Task<List<Unit>> GetUnits(UnitsSortOrder? sortOrder);
}

public enum UnitsSortOrder
{
    Name,
    Name_Desc,
    Type,
    Type_Desc
}

public class UnitRepository(AppDbContext dbContext) : IUnitRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<List<Unit>> GetUnits(UnitsSortOrder? sortOrder)
    {
        sortOrder ??= UnitsSortOrder.Name;

        switch (sortOrder)
        {
            case UnitsSortOrder.Name:
                return await _dbContext.Units.OrderBy(
                    u => u.Name
                ).ToListAsync();

            case UnitsSortOrder.Name_Desc:
                return await _dbContext.Units.OrderByDescending(
                    u => u.Name
                ).ToListAsync();

            case UnitsSortOrder.Type:
                return await _dbContext.Units.OrderBy(
                    u => u.Type
                ).ToListAsync();

            case UnitsSortOrder.Type_Desc:
                return await _dbContext.Units.OrderByDescending(
                    u => u.Type
                ).ToListAsync();

            default:
                throw new ApplicationException();
        }
    }
}