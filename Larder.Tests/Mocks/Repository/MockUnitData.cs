using Larder.Models;
using Larder.Repository.Impl;
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

        units.AddRange([grams, milligrams, pounds]);
    }

    public Task Delete(Unit entity)
    {
        throw new NotImplementedException();
    }

    public Task<Unit?> Get(string userId, string id)
    {
        Unit? unit = units.FirstOrDefault(u =>
            u.UserId == userId && u.Id == id);

        return Task<Unit?>.FromResult(unit);
    }

    public Task<List<Unit>> GetAll(string userId, UnitSortOptions sortBy, string? search)
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