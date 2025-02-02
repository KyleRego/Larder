using Larder.Models;
using Larder.Repository.Impl;

namespace Larder.Repository.Interface;

public interface IUnitRepository : IRepositoryBase<Unit, UnitSortOptions>
{
    public Task<List<Unit>> InsertAll(List<Unit> units);
}
