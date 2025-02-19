using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Repository.Interface;

public interface IUnitConversionRepository
                : IRepositoryBase<UnitConversion>
{
    public Task<UnitConversion?> FindByUnitIdsEitherWay(string userId,
                                                        string unitId1,
                                                        string unitId2);
}
