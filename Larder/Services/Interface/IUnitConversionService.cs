using Larder.Dtos;
using Larder.Models;

namespace Larder.Services.Interface;

public interface IUnitConversionService
{
    public Task<UnitConversionDto> CreateUnitConversion(UnitConversionDto dto);
    public Task<UnitConversionDto> UpdateUnitConversion(UnitConversionDto dto);
    public Task DeleteUnitConversion(string id);
    public Task<UnitConversionDto?> FindConversion(string unitId1, string unitId2);
}
