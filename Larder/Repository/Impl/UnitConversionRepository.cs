using Larder.Data;
using Larder.Models;
using Microsoft.EntityFrameworkCore;
using Larder.Repository.Interface;

namespace Larder.Repository.Impl;

public class UnitConversionRepository(AppDbContext dbContext)
        : RepositoryBase<UnitConversion>(dbContext),
                                IUnitConversionRepository
{
    public async Task<UnitConversion?> FindByUnitIdsEitherWay(string userId,
                                                                string unitId1,
                                                                string unitId2)
    {
        return await _dbContext.UnitConversions
                    .Include(uc => uc.Unit)
                    .Include(uc => uc.TargetUnit)
                    .FirstOrDefaultAsync(uc => uc.UserId == userId &&
            ((uc.UnitId == unitId1 && uc.TargetUnitId == unitId2) ||
                (uc.UnitId == unitId2 && uc.TargetUnitId == unitId1))
        );
    }

    public async override Task<UnitConversion?> Get(string userId, string id)
    {
        return await _dbContext.UnitConversions
                    .Include(uc => uc.Unit)
                    .Include(uc => uc.TargetUnit).FirstOrDefaultAsync(
                                uc => uc.Id == id && uc.UserId == userId); 
    }
}
