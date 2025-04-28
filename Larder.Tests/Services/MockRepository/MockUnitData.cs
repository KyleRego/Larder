using Larder.Models;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Services.MockRepository;

public class MockUnitData : MockCrudRepositoryBase<Unit>, IUnitRepository
{
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
            _records.Add(new Unit(testUserId, name, type)
            {
                Id = name.Replace(" ", "-")
            });
        }
    }

    public Task<List<Unit>> GetAll(string userId, UnitSortOptions sortOption = UnitSortOptions.AnyOrder, string? search = null)
    {
        throw new NotImplementedException();
    }

    public Task<Unit?> GetUnitOnly(string userId, string id)
    {
        Unit? unit = _records.FirstOrDefault(u =>
            u.UserId == userId && u.Id == id);

        if (unit != null)
            Assert.Empty(unit.Conversions);

        return Task.FromResult(unit);
    }
}
