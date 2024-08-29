using Larder.Data;
using Larder.Models;
using Microsoft.EntityFrameworkCore;

namespace Larder.Repository;

public enum UnitConversionSortOptions
{
    AnyOrder,
    UnitType
}

public interface IUnitConversionRepository
                : IRepositoryBase<UnitConversion, UnitConversionSortOptions>
{
    public Task<UnitConversion?> FindByUnitIdsEitherWay(string unitId1, string unitId2);
}

public class UnitConversionRepository(AppDbContext dbContext)
            : RepositoryBase<UnitConversion, UnitConversionSortOptions>(dbContext), IUnitConversionRepository
{
    public async Task<UnitConversion?> FindByUnitIdsEitherWay(string unitId1, string unitId2)
    {
        return await _dbContext.UnitConversions
                    .Include(uc => uc.Unit)
                    .Include(uc => uc.TargetUnit)
                    .FirstOrDefaultAsync(uc =>
            (uc.UnitId == unitId1 && uc.TargetUnitId == unitId2) || (uc.UnitId == unitId2 && uc.TargetUnitId == unitId1)
        );
    }

    public async override Task<UnitConversion?> Get(string id)
    {
        return await _dbContext.UnitConversions
                    .Include(uc => uc.Unit)
                    .Include(uc => uc.TargetUnit).FirstOrDefaultAsync(uc => uc.Id == id); 
    }

    public override Task<List<UnitConversion>> GetAll(UnitConversionSortOptions sortBy, string? search)
    {
        throw new NotImplementedException();
    }
}
