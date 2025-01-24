using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IUnitConversionService
{
    public Task<UnitConversionDto> CreateUnitConversion(UnitConversionDto dto);
    public Task<UnitConversionDto> UpdateUnitConversion(UnitConversionDto dto);
    public Task DeleteUnitConversion(string id);
    public Task<UnitConversionDto?> FindConversion(Quantity first, Quantity second);
}

public class UnitConversionService(
                                IServiceProviderWrapper serviceProvider,
                                IUnitRepository unitData,
                                IUnitConversionRepository unitConversionData)
                    : AppServiceBase(serviceProvider), IUnitConversionService
{
    private readonly IUnitRepository _unitData = unitData;
    private readonly IUnitConversionRepository _unitConversionData
                                                        = unitConversionData;

    private static void CheckConversionValid(Unit unit1, Unit unit2)
    {
        if (unit1.Type != unit2.Type) {
            throw new ApplicationException($"""
            A unit conversion cannot be created for {unit1.Name} and
            {unit2.Name} 
            because they are different types ({unit1.Type} and {unit2.Type}).
        """);
        }

        if (unit1.Id == unit2.Id) {
            throw new ApplicationException(
                "A conversion cannot be created between a unit and itself."
            );
        }      
    }

    public async Task<UnitConversionDto>
                                    CreateUnitConversion(UnitConversionDto dto)
    {
        double targetUnitsPerUnit = dto.TargetUnitsPerUnit;
        string userId = CurrentUserId();

        Unit unit = await _unitData.Get(userId, dto.UnitId)
                ?? throw new ApplicationException(
                    $"Unit with id {dto.UnitId} not found.");

        Unit targetUnit = await _unitData.Get(userId, dto.TargetUnitId)
                ?? throw new ApplicationException(
                    $"Unit with id {dto.TargetUnitId} not found.");

        CheckConversionValid(unit, targetUnit);

        UnitConversion? existingConversion = await _unitConversionData
                    .FindByUnitIdsEitherWay(userId, unit.Id, targetUnit.Id);

        if (existingConversion != null)
            throw new ApplicationException(
                "A conversion already exists for those units.");

        UnitConversion unitConversion
            = new(userId, unit.Id, targetUnit.Id, targetUnitsPerUnit)
        {
            UnitType = targetUnit.Type
        };

        await _unitConversionData.Insert(unitConversion);

        return UnitConversionDto.FromEntity(unitConversion);
    }

    public async Task DeleteUnitConversion(string id)
    {
        UnitConversion unitConversion = await _unitConversionData.Get(CurrentUserId(), id)
            ?? throw new ApplicationException("Unit conversion to update not found.");

        await _unitConversionData.Delete(unitConversion);
    }

    public async Task<UnitConversionDto?> FindConversion(Quantity first, Quantity second)
    {
        if (first.UnitId == null || second.UnitId == null)
            throw new ApplicationException("There must be units to find a conversion for");

        UnitConversion? conversion = 
                    await _unitConversionData.FindByUnitIdsEitherWay(
                        CurrentUserId(), first.UnitId, second.UnitId);
        return (conversion != null) ? UnitConversionDto.FromEntity(conversion) : null;
    }

    public async Task<UnitConversionDto> UpdateUnitConversion(UnitConversionDto dto)
    {
        if (dto.Id == null) throw new ApplicationException(
            "Id of unit conversion to update was missing.");

        UnitConversion unitConversion = await _unitConversionData.Get(CurrentUserId(), dto.Id)
                ?? throw new ApplicationException(
                    "Unit conversion to update was not found.");

        Unit unit = await _unitData.Get(CurrentUserId(), dto.UnitId)
                ?? throw new ApplicationException("Unit not found");

        Unit targetUnit = await _unitData.Get(CurrentUserId(), dto.TargetUnitId)
                ?? throw new ApplicationException("Target unit not found");

        CheckConversionValid(unit, targetUnit);

        unitConversion.UnitId = unit.Id;
        unitConversion.TargetUnitId = targetUnit.Id;
        unitConversion.TargetUnitsPerUnit = dto.TargetUnitsPerUnit;

        await _unitConversionData.Update(unitConversion);

        return UnitConversionDto.FromEntity(unitConversion);
    }
}
