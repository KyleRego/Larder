using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

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

public class UnitService(IServiceProviderWrapper serviceProvider,
                                                    IUnitRepository repository)
                        : AppServiceBase(serviceProvider), IUnitService
{
    private readonly IUnitRepository _repository = repository;

    public async Task<UnitDto> CreateUnit(UnitDto dto)
    {
        Unit unit = new(CurrentUserId(), dto.Name, dto.Type); 

        Unit insertedUnit = await _repository.Insert(unit);

        return UnitDto.FromEntity(insertedUnit);
    }

    public async Task DeleteUnit(string id)
    {
        Unit unit = await _repository.Get(CurrentUserId(), id)
            ?? throw new ApplicationException("Unit was not found.");

        await _repository.Delete(unit);
    }

    public async Task<UnitDto?> GetUnit(string id)
    {
        Unit? entity = await _repository.Get(CurrentUserId(), id);

        if (entity == null) return null;

        return UnitDto.FromEntity(entity);
    }

    public async Task<List<UnitDto>> GetUnits(UnitSortOptions sortOrder,
                                                                string? search)
    {
        List<Unit> units =
            await _repository.GetAll(CurrentUserId(), sortOrder, search);

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

        Unit entity = await _repository.Get(CurrentUserId(), dto.Id)
            ?? throw new ApplicationException("unit not found");

        entity.Name = dto.Name;
        entity.Type = dto.Type;

        Unit updatedUnit = await _repository.Update(entity);

        return UnitDto.FromEntity(updatedUnit);
    }
}
