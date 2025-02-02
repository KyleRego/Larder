using Larder.Dtos;
using Larder.Repository;
using Larder.Repository.Impl;

namespace Larder.Services.Interface;

public interface IUnitService
{
    public Task<UnitDto?> GetUnit(string id);
    public Task<List<UnitDto>> GetUnits(UnitSortOptions sortOrder,
                                                    string? search);
    public Task<UnitDto> CreateUnit(UnitDto dto);
    public Task<IEnumerable<UnitDto>> CreateUnits(IEnumerable<UnitDto> unitDtos);
    public Task<UnitDto> UpdateUnit(UnitDto dto);
    public Task DeleteUnit(string id);
}
