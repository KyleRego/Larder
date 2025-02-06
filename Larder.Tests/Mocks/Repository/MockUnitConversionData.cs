using Larder.Models;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Mocks.Repository;

public class MockUnitConversionData : MockRepositoryBase,
                                        IUnitConversionRepository
{
    private readonly List<UnitConversion> unitConversions = [];

    public MockUnitConversionData()
    {
        MockUnitData unitData = new();

        Unit grams = unitData.Get(testUserId, "grams").GetAwaiter().GetResult()!;
        Unit milligrams = unitData.Get(testUserId, "milligrams").GetAwaiter().GetResult()!;

        UnitConversion gramsToMilligrams = new(testUserId, grams.Id, milligrams.Id, 1000)
        {
            UnitType = UnitType.Mass,
            Id = "grams-to-milligrams"
        };

        unitConversions.AddRange([gramsToMilligrams]);
    }

    public Task Delete(UnitConversion entity)
    {
        throw new NotImplementedException();
    }

    public Task<UnitConversion?> FindByUnitIdsEitherWay(string userId, string unitId1, string unitId2)
    {
        UnitConversion? result = unitConversions.FirstOrDefault(uc => uc.UserId == userId &&
            (uc.UnitId == unitId1 && uc.TargetUnitId == unitId2 ||
            uc.UnitId == unitId2 && uc.TargetUnitId == unitId1)
        );

        return Task<UnitConversion?>.FromResult(result);
    }

    public Task<UnitConversion?> Get(string userId, string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<UnitConversion>> GetAll(string userId, UnitConversionSortOptions sortBy, string? search)
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