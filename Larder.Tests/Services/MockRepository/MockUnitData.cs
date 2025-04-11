using Larder.Models;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Services.MockRepository;

public class MockUnitData : MockRepositoryBase, IUnitRepository
{
    private readonly List<Unit> units = [];

    public MockUnitData()
    {
        var unitTups = new (string Name, UnitType Type)[]
        {
            ("grams", UnitType.Mass),
            ("milligrams", UnitType.Mass),
            ("pounds", UnitType.Weight),
            ("bread slices", UnitType.Mass),
            ("butter sticks", UnitType.Volume),
            ("tablespoons", UnitType.Volume),
            ("cups", UnitType.Volume)
        };

        foreach (var (name, type) in unitTups)
        {
            units.Add(new Unit(testUserId, name, type)
            {
                Id = name.Replace(" ", "-")
            });
        }
    }

    public Task Delete(Unit entity)
    {
        throw new NotImplementedException();
    }

    public Task<Unit?> GetOrNull(string userId, string id)
    {
        Unit? unit = units.FirstOrDefault(u =>
            u.UserId == userId && u.Id == id);

        return Task.FromResult(unit);
    }

    public Task<Unit> Get(string userId, string id)
    {
        return GetOrNull(userId, id)!;
    }

    public Task<List<Unit>> GetAll(string userId, UnitSortOptions sortOption = UnitSortOptions.AnyOrder, string? search = null)
    {
        throw new NotImplementedException();
    }

    public Task<Unit?> GetUnitOnly(string userId, string id)
    {
        Unit? unit = units.FirstOrDefault(u =>
            u.UserId == userId && u.Id == id);

        if (unit != null)
            Assert.Empty(unit.Conversions);

        return Task.FromResult(unit);
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