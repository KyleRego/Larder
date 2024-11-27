using Larder.Dtos;
using Larder.Repository;

namespace Larder.Services;

public interface IUnitService
{
    public Task<UnitDto?> GetUnit(string id);
    public Task<List<UnitDto>> GetUnits(UnitSortOptions sortOrder,
                                                    string? search);
    public Task<UnitDto> CreateUnit(UnitDto dto);
    public Task<UnitDto> UpdateUnit(UnitDto dto);
    public Task DeleteUnit(string id);
}
