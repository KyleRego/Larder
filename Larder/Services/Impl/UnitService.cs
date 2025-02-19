using System.Linq;
using Larder.Dtos;
using Larder.Models;
using Larder.Models.SortOptions;
using Larder.Repository.Impl;
using Larder.Repository.Interface;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class UnitService(IServiceProviderWrapper serviceProvider,
                                                    IUnitRepository repository)
                        : AppServiceBase(serviceProvider), IUnitService
{
    private readonly IUnitRepository _unitData = repository;

    public async Task<UnitDto> CreateUnit(UnitDto dto)
    {
        Unit unit = new(CurrentUserId(), dto.Name, dto.Type); 

        Unit insertedUnit = await _unitData.Insert(unit);

        return UnitDto.FromEntity(insertedUnit);
    }

    public async Task<List<UnitDto>> CreateUnits(List<UnitDto> unitDtos)
    {
        List<Unit> units = [.. unitDtos.Select<UnitDto, Unit>
                (dto => new(CurrentUserId(), dto.Name, dto.Type))];

        List<Unit> insertedUnits = await _unitData.InsertAll(units);

        return [ .. insertedUnits.Select(UnitDto.FromEntity) ];
    }

    public async Task DeleteUnit(string id)
    {
        Unit unit = await _unitData.Get(CurrentUserId(), id)
            ?? throw new ApplicationException("Unit was not found.");

        await _unitData.Delete(unit);
    }

    public async Task<UnitDto?> GetUnit(string id)
    {
        Unit? entity = await _unitData.Get(CurrentUserId(), id);

        if (entity == null) return null;

        return UnitDto.FromEntity(entity);
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

    public async Task<UnitDto> UpdateUnit(UnitDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.Id);

        Unit entity = await _unitData.Get(CurrentUserId(), dto.Id)
            ?? throw new ApplicationException("unit not found");

        entity.Name = dto.Name;
        entity.Type = dto.Type;

        Unit updatedUnit = await _unitData.Update(entity);

        return UnitDto.FromEntity(updatedUnit);
    }
}
