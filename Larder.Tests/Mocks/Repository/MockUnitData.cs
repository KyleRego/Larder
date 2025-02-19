using Larder.Models;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Mocks.Repository;

public class MockUnitData : MockRepositoryBase, IUnitRepository
{
    private readonly List<Unit> units = [];

    public MockUnitData()
    {
        Unit grams = new(testUserId, "grams", UnitType.Mass)
        {
            Id = "grams"
        };
        Unit milligrams = new(testUserId, "milligrams", UnitType.Mass)
        {
            Id = "milligrams"
        };
        Unit pounds = new(testUserId, "pounds", UnitType.Weight)
        {
            Id = "pounds"
        };
        Unit breadSlices = new(testUserId, "bread slices", UnitType.Mass)
        {
            Id="bread-slices"
        };
        Unit butterSticks = new(testUserId, "butter sticks", UnitType.Mass)
        {
            Id="butter-sticks"
        };

        units.AddRange([grams, milligrams, pounds, breadSlices, butterSticks]);
    }

    public Task Delete(Unit entity)
    {
        throw new NotImplementedException();
    }

    public Task<Unit?> Get(string userId, string id)
    {
        Unit? unit = units.FirstOrDefault(u =>
            u.UserId == userId && u.Id == id);

        return Task.FromResult(unit);
    }

    public Task<List<Unit>> GetAll(string userId, UnitSortOptions sortOption = UnitSortOptions.AnyOrder, string? search = null)
    {
        throw new NotImplementedException();
    }

    public Task<Unit> Insert(Unit newEntity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Unit>> InsertAll(List<Unit> newEntities)
    {
        throw new NotImplementedException();
    }

    public Task<Unit> Update(Unit editedEntity)
    {
        throw new NotImplementedException();
    }
}