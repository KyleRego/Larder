using Larder.Dtos;
using Larder.Models;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class UnitService(IServiceProviderWrapper serviceProvider,
                            IUnitRepository unitData)
        : CrudServiceBase<UnitDto, Unit>(serviceProvider, unitData),
                        IUnitService
{
    private readonly IUnitRepository _unitData = unitData;

    public async Task<List<UnitDto>> CreateUnits(List<UnitDto> unitDtos)
    {
        List<Unit> units = [.. unitDtos.Select<UnitDto, Unit>
                (dto => new(CurrentUserId(), dto.Name, dto.Type))];

        List<Unit> insertedUnits = await _unitData.InsertAll(units);

        return [ .. insertedUnits.Select(UnitDto.FromEntity) ];
    }

    public async Task<UnitType> GetUnitType(string unitId)
    {
        Unit unit = await _unitData.GetUnitOnly(CurrentUserId(), unitId)
            ?? throw new ApplicationException($"No unit found with ID {unitId}");

        return unit.Type;
    }

    public async Task<List<UnitDto>> GetUnits(UnitSortOptions sortOrder,
                                                                string? search)
    {
        List<Unit> units =
            await _unitData.GetAll(CurrentUserId(), sortOrder, search);

        List<UnitDto> dtos = [];

        foreach (Unit unit in units)
        {
            dtos.Add(UnitDto.FromEntity(unit));
        }

        return dtos;
    }

    protected override UnitDto MapToDto(Unit entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Type = entity.Type,
            Conversions = [.. entity.Conversions.Select(UnitConversionDto.FromEntity)]
        };
    }

    protected override Task<Unit> MapToEntity(UnitDto dto)
    {
        string userId = CurrentUserId();
        Unit unit = new(userId, dto.Name, dto.Type)
        {
            Conversions = [.. dto.Conversions.Select(convDto =>
                new UnitConversion(userId, convDto.UnitId,
                            convDto.TargetUnitId, convDto.TargetUnitsPerUnit)
            )]
        };

        if (dto.Id != null)
            unit.Id = dto.Id;

        return Task.FromResult(unit);
    }
}
