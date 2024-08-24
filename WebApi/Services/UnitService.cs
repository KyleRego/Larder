using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Microsoft.OpenApi.Extensions;

namespace Larder.Services;

public interface IUnitService
{
    public Task<UnitDto?> GetUnit(string id);

    public Task<List<UnitDto>> GetUnits(UnitSortOptions sortOrder, string? search);

    public Task CreateUnit(UnitDto dto);

    public Task UpdateUnit(UnitDto dto);
}

public class UnitService(IUnitRepository rep) : IUnitService
{
    private readonly IUnitRepository _rep = rep;

    public async Task CreateUnit(UnitDto dto)
    {
        Unit entity = new()
        {
            Name = dto.Name,
            Type = dto.Type
        };

        await _rep.Insert(entity);
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

    public async Task UpdateUnit(UnitDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.Id);

        Unit? entity = await _rep.Get(dto.Id);

        if (entity == null) throw new ApplicationException("unit not found");

        entity.Name = dto.Name;
        entity.Type = dto.Type;

        await _rep.Update(entity);
    }
}