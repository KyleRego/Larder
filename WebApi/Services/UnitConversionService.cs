using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IUnitConversionService
{
    public Task<UnitConversionDto> CreateUnitConversion(UnitConversionDto dto);
    public Task<UnitConversionDto> UpdateUnitConversion(UnitConversionDto dto);
    public Task DeleteUnitConversion(string id);
}

public class UnitConversionService(IServiceProviderWrapper serviceProvider,
                                        IUnitRepository unitRep,
                                        IUnitConversionRepository unitConvRep)
            : AppServiceBase(serviceProvider), IUnitConversionService
{
    private readonly IUnitRepository _unitRep = unitRep;
    private readonly IUnitConversionRepository _unitConvRep = unitConvRep;

    public async Task<UnitConversionDto>
                                    CreateUnitConversion(UnitConversionDto dto)
    {
        Unit unit = await _unitRep.Get(dto.UnitId)
                ?? throw new ApplicationException("unit not found");

        await ThrowIfUserCannotAccess(unit);

        Unit targetUnit = await _unitRep.Get(dto.TargetUnitId)
                ?? throw new ApplicationException("target unit not found");

        await ThrowIfUserCannotAccess(targetUnit);

        if (unit.Type != targetUnit.Type)
            throw new ApplicationException("unit and target unit must be same type (e.g. mass units)");
    
        UnitConversion unitConversion = new()
        {
            UserId = CurrentUserId(),
            UnitId = unit.Id,
            TargetUnitId = targetUnit.Id,
            UnitType = unit.Type,
            TargetUnitsPerUnit = dto.TargetUnitsPerUnit
        };

        await _unitConvRep.Insert(unitConversion);

        return UnitConversionDto.FromEntity(unitConversion);
    }

    public async Task DeleteUnitConversion(string id)
    {
        UnitConversion unitConversion = await _unitConvRep.Get(id)
            ?? throw new ApplicationException("unit conversion to update not found");

        await ThrowIfUserCannotAccess(unitConversion);

        await _unitConvRep.Delete(unitConversion);
    }

    public async Task<UnitConversionDto> UpdateUnitConversion(UnitConversionDto dto)
    {
        if (dto.Id == null) throw new ApplicationException("unit conversion id was missing");

        UnitConversion unitConversion = await _unitConvRep.Get(dto.Id)
                ?? throw new ApplicationException("unit conversion to update not found");

        Unit unit = await _unitRep.Get(dto.UnitId)
                ?? throw new ApplicationException("unit not found");

        Unit targetUnit = await _unitRep.Get(dto.TargetUnitId)
                ?? throw new ApplicationException("target unit not found");

        if (unit.Type != targetUnit.Type)
                throw new ApplicationException("unit and target unit must be same type (e.g. mass units)");

        unitConversion.UnitId = unit.Id;
        unitConversion.TargetUnitId = targetUnit.Id;
        unitConversion.TargetUnitsPerUnit = dto.TargetUnitsPerUnit;

        await _unitConvRep.Update(unitConversion);

        return UnitConversionDto.FromEntity(unitConversion);
    }
}
