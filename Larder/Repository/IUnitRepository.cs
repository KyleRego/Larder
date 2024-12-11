using Larder.Models;

namespace Larder.Repository;

public interface IUnitRepository : IRepositoryBase<Unit, UnitSortOptions>
{
    public Task InsertAll(IEnumerable<Unit> units);
}
