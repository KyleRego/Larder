using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Larder.Services;

public interface IUnitService
{
    public Task<UnitDto?> GetUnit(string id);
    public Task<List<UnitDto>> GetUnits(UnitSortOptions sortOrder,
                                                    string? search);
    public Task<UnitDto> CreateUnit(UnitDto dto);
    public Task<UnitDto> UpdateUnit(UnitDto dto);
    public Task DeleteUnit(string id);
}

public class UnitService(IUnitRepository repository,
                        IHttpContextAccessor httpConAcsr,
                        IAuthorizationService authService)
        : ApplicationServiceBase(httpConAcsr, authService), IUnitService
{
    private readonly IUnitRepository _repository = repository;

    public async Task<UnitDto> CreateUnit(UnitDto dto)
    {
        Unit entity = new()
        {
            UserId = CurrentUserId(),
            Name = dto.Name,
            Type = dto.Type
        };

        Unit insertedUnit = await _repository.Insert(entity);

        return UnitDto.FromEntity(insertedUnit);
    }

    public async Task DeleteUnit(string id)
    {
        Unit unit = await _repository.Get(id)
            ?? throw new ApplicationException("unit not found");

        await ThrowIfUserCannotAccess(unit);

        await _repository.Delete(unit);
    }

    public async Task<UnitDto?> GetUnit(string id)
    {
        Unit? entity = await _repository.Get(id);

        if (entity == null) return null;

        await ThrowIfUserCannotAccess(entity);

        return UnitDto.FromEntity(entity);
    }

    public async Task<List<UnitDto>> GetUnits(UnitSortOptions sortOrder,
                                                                string? search)
    {
        List<Unit> units =
            await _repository.GetAllForUser(CurrentUserId(), sortOrder, search);

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

        Unit entity = await _repository.Get(dto.Id)
            ?? throw new ApplicationException("unit not found");

        await ThrowIfUserCannotAccess(entity);

        entity.Name = dto.Name;
        entity.Type = dto.Type;

        Unit updatedUnit = await _repository.Update(entity);

        return UnitDto.FromEntity(updatedUnit);
    }
}