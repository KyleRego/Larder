using Larder.Dtos;
using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Services.Interface;

public interface IUnitService : ICrudServiceBase<UnitDto, Unit>
{
    public Task<List<UnitDto>> GetUnits(UnitSortOptions sortOrder,
                                                    string? search);
    public Task<List<UnitDto>> CreateUnits(List<UnitDto> unitDtos);
}
