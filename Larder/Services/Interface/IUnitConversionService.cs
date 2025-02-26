using Larder.Dtos;
using Larder.Models;

namespace Larder.Services.Interface;

public interface IUnitConversionService
    : ICrudServiceBase<UnitConversionDto, UnitConversion>
{
    public Task<UnitConversionDto?>
        FindConversion(string unitId1, string unitId2);
}
