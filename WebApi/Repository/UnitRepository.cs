using Microsoft.EntityFrameworkCore;
using Larder.Data;
using Larder.Models;

namespace Larder.Repository;

public interface IUnitRepository
{
    public Task<List<Unit>> GetUnits(string sortOrder);
}

public class UnitRepository(AppDbContext dbContext) : IUnitRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<List<Unit>> GetUnits(string sortOrder)
    {
        if (sortOrder == "Name")
        {
            return await _dbContext.Units.OrderBy(
                u => u.Name
            ).ToListAsync();
        }
        else if (sortOrder == "Name_Desc")
        {
            return await _dbContext.Units.OrderByDescending(
                u => u.Name
            ).ToListAsync();
        }
        else
        {
            throw new ArgumentException("sortOrder was not a supported string");
        }
    }
}