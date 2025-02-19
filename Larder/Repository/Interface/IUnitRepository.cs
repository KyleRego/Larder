using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Repository.Interface;

public interface IUnitRepository : IRepositoryBase<Unit>
{
    public Task<List<Unit>> GetAll(string userId,
                UnitSortOptions sortOption=UnitSortOptions.AnyOrder,
                string? search = null);
}
