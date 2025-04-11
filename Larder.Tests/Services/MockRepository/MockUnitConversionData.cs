using Larder.Models;
using Larder.Repository.Interface;

namespace Larder.Tests.Services.MockRepository;

public class MockUnitConversionData : MockRepositoryBase,
                                        IUnitConversionRepository
{
    private readonly List<UnitConversion> unitConversions = [];
    private readonly IUnitRepository _unitData;
    public MockUnitConversionData(IUnitRepository unitData)
    {
        _unitData = unitData;

        Unit grams = _unitData.Get(testUserId, "grams")
                    .GetAwaiter().GetResult()!;
        Unit milligrams = _unitData.Get(testUserId, "milligrams")
                    .GetAwaiter().GetResult()!;
        Unit tablespoons = _unitData.Get(testUserId, "tablespoons")
                    .GetAwaiter().GetResult()!;
        Unit butterSticks = _unitData.Get(testUserId, "butter-sticks")
                    .GetAwaiter().GetResult()!;

        UnitConversion gramsToMilligrams =
            new(testUserId, grams.Id, milligrams.Id, 1000)
            {
                UnitType = UnitType.Mass,
                Id = "grams-to-milligrams"
            };

        UnitConversion tablespoonsToButtersticks =
            new(testUserId, butterSticks.Id, tablespoons.Id, 8)
            {
                UnitType = UnitType.Volume,
                Id = "butter-sticks-to-tablespoons"
            };

        UnitConversion gramsToButtersticks =
            new(testUserId, butterSticks.Id, grams.Id, 14 * 8)
            {
                UnitType = UnitType.Mass,
                Id = "butter-sticks-to-grams"
            };

        unitConversions.AddRange(
            [gramsToMilligrams,
            tablespoonsToButtersticks,
            gramsToButtersticks]);
    }

    public Task Delete(UnitConversion entity)
    {
        throw new NotImplementedException();
    }

    public Task<UnitConversion?> FindByUnitIdsEitherWay
        (string userId, string unitId1, string unitId2)
    {
        UnitConversion? result = unitConversions
            .FirstOrDefault(uc => uc.UserId == userId &&
                (uc.UnitId == unitId1 && uc.TargetUnitId == unitId2 ||
                uc.UnitId == unitId2 && uc.TargetUnitId == unitId1)
        );

        return Task.FromResult(result);
    }

    public Task<UnitConversion> Get(string userId, string id)
    {
        throw new NotImplementedException();
    }

    public Task<UnitConversion?> GetOrNull(string userId, string id)
    {
        throw new NotImplementedException();
    }

    public Task<UnitConversion> Insert(UnitConversion newEntity)
    {
        throw new NotImplementedException();
    }

    public Task<List<UnitConversion>> InsertAll(List<UnitConversion> newEntities)
    {
        throw new NotImplementedException();
    }

    public Task<UnitConversion> Update(UnitConversion editedEntity)
    {
        throw new NotImplementedException();
    }
}