using Larder.Dtos;
using Larder.Models.SortOptions;

namespace Larder.Services.Interface;

public interface IUnitService
{
    public Task<UnitDto?> GetUnit(string id);
    public Task<List<UnitDto>> GetUnits(UnitSortOptions sortOrder,
                                                    string? search);
    public Task<UnitDto> CreateUnit(UnitDto dto);
    public Task<List<UnitDto>> CreateUnits(List<UnitDto> unitDtos);
    public Task<UnitDto> UpdateUnit(UnitDto dto);
    public Task DeleteUnit(string id);
}
