using Larder.Models;
using Larder.Repository.Interface;

namespace Larder.Tests.Services.MockRepository;

public class MockUnitConversionData : MockCrudRepositoryBase<UnitConversion>,
                                        IUnitConversionRepository
{
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

        _records.AddRange(
            [gramsToMilligrams,
            tablespoonsToButtersticks,
            gramsToButtersticks]);
    }

    public Task<UnitConversion?> FindByUnitIdsEitherWay
        (string userId, string unitId1, string unitId2)
    {
        UnitConversion? result = _records
            .FirstOrDefault(uc => uc.UserId == userId &&
                (uc.UnitId == unitId1 && uc.TargetUnitId == unitId2 ||
                uc.UnitId == unitId2 && uc.TargetUnitId == unitId1)
        );

        return Task.FromResult(result);
    }
}
