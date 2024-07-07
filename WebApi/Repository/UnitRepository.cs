using Microsoft.EntityFrameworkCore;
using Larder.Data;
using Larder.Models;

namespace Larder.Repository;

public interface IUnitRepository
{
    public Task<List<Unit>> GetUnits();
}

public class UnitRepository(AppDbContext dbContext) : IUnitRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<List<Unit>> GetUnits()
    {
        return await _dbContext.Units.ToListAsync();
    }
}