using Larder.Dtos;
using Larder.Models;
using Larder.Repository.Interface;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class UnitConversionService(
                                IServiceProviderWrapper serviceProvider,
                                IUnitService unitService,
                                IUnitConversionRepository unitConversionData)
        : CrudServiceBase<UnitConversionDto, UnitConversion>
            (serviceProvider, unitConversionData),
        IUnitConversionService
{
    private readonly IUnitService _unitService = unitService;
    private readonly IUnitConversionRepository _unitConversionData
                                                        = unitConversionData;

    public async Task<UnitConversionDto?> FindConversion(string unitId1, string unitId2)
    {
        UnitConversion? conversion = 
                    await _unitConversionData.FindByUnitIdsEitherWay(
                        CurrentUserId(), unitId1, unitId2);

        if (conversion == null) return null;

        return  UnitConversionDto.FromEntity(conversion);
    }

    protected override UnitConversionDto MapToDto(UnitConversion entity)
    {
        return new()
        {
            Id = entity.Id,
            UnitId = entity.UnitId,
            TargetUnitId = entity.TargetUnitId,
            TargetUnitsPerUnit = entity.TargetUnitsPerUnit
        };
    }

    protected async override Task<UnitConversion> MapToEntity(UnitConversionDto dto)
    {
        string unitId = dto.UnitId;
        string targetUnitId = dto.TargetUnitId;

        if (string.IsNullOrWhiteSpace(unitId) || string.IsNullOrWhiteSpace(targetUnitId))
        {
            throw new ApplicationException("A unit conversion must be between two units");
        } else if (unitId == targetUnitId)
        {
            throw new ApplicationException("A unit conversion cannot be made for a unit and itself");
        }

        UnitType unitType1 = await _unitService.GetUnitType(unitId);
        UnitType unitType2 = await _unitService.GetUnitType(targetUnitId);

        if (unitType1 != unitType2)
            throw new ApplicationException("A unit conversion cannot be created between units of different types");

        UnitConversion entity = new(CurrentUserId(), unitId,
                        targetUnitId!, dto.TargetUnitsPerUnit);

        if (dto.Id != null)
            entity.Id = dto.Id;

        return entity;
    }
}
