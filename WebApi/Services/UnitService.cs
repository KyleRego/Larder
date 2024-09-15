using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IUnitService
{
    public Task<UnitDto?> GetUnit(string id);
    public Task<List<UnitDto>> GetUnits(UnitSortOptions sortOrder, string? search);
    public Task<UnitDto> CreateUnit(UnitDto dto);
    public Task<UnitDto> UpdateUnit(UnitDto dto);
    public Task DeleteUnit(string id);
}

public class UnitService(IUnitRepository rep) : IUnitService
{
    private readonly IUnitRepository _rep = rep;

    public async Task<UnitDto> CreateUnit(UnitDto dto)
    {
        Unit entity = new()
        {
            Name = dto.Name,
            Type = dto.Type
        };

        Unit insertedUnit = await _rep.Insert(entity);

        return UnitDto.FromEntity(insertedUnit);
    }

    public async Task DeleteUnit(string id)
    {
        Unit unit = await _rep.Get(id) ?? throw new ApplicationException("unit not found");
    
        await _rep.Delete(unit);
    }

    public async Task<UnitDto?> GetUnit(string id)
    {
        Unit? entity = await _rep.Get(id);

        return (entity == null) ? null : UnitDto.FromEntity(entity);
    }

    public async Task<List<UnitDto>> GetUnits(UnitSortOptions sortOrder, string? search)
    {
        List<Unit> units = await _rep.GetAll(sortOrder, search);

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

        Unit entity = await _rep.Get(dto.Id) ?? throw new ApplicationException("unit not found");

        entity.Name = dto.Name;
        entity.Type = dto.Type;

        Unit updatedUnit = await _rep.Update(entity);

        return UnitDto.FromEntity(updatedUnit);
    }
}