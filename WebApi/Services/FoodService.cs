using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IFoodService
{
    public Task<FoodDto?> GetFood(string id);

    public Task<List<FoodDto>> GetFoods(FoodSortOptions sortOrder, string? search);

    public Task<FoodDto> CreateFood(FoodDto food);

    public Task<FoodDto> UpdateFood(FoodDto food);

    public Task<FoodDto> UpdateQuantity(QuantityDto quantity);
}

public class FoodService(IFoodRepository foodRepo) : IFoodService
{
    private readonly IFoodRepository _foodRepo = foodRepo;

    public async Task<FoodDto> CreateFood(FoodDto dto)
    {
        Food entity = new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Calories = dto.Calories,
            Amount = dto.Amount,
            UnitId = string.IsNullOrWhiteSpace(dto.UnitId) ? null : dto.UnitId
        };

        await _foodRepo.Insert(entity);

        return dto;
    }

    public async Task<FoodDto> UpdateFood(FoodDto dto)
    {
        if (dto.Id == null) throw new ApplicationException("food id was missing");

        Food entity = await _foodRepo.Get(dto.Id)
                            ?? throw new ApplicationException("food was not found");

        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.Calories = dto.Calories;
        entity.Amount = dto.Amount;
        entity.UnitId = string.IsNullOrWhiteSpace(dto.UnitId) ? null : dto.UnitId;

        await _foodRepo.Update(entity);

        return dto;
    }

    public async Task<FoodDto?> GetFood(string id)
    {
        Food? entity = await _foodRepo.Get(id);

        if (entity == null) return null;

        return FoodDtoAssembler.Assemble(entity);
    }

    public async Task<List<FoodDto>> GetFoods(FoodSortOptions sortBy, string? search)
    {
        List<Food> foods = await _foodRepo.GetAll(sortBy, search);

        List<FoodDto> foodDtos = [];

        foreach (Food food in foods)
        {
            foodDtos.Add(FoodDtoAssembler.Assemble(food));
        }

        return foodDtos;
    }

    public async Task<FoodDto> UpdateQuantity(QuantityDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.Id);

        Food entity = await _foodRepo.Get(dto.Id) ?? throw new ApplicationException("food not found");

        entity.Amount = dto.Amount;
        entity.UnitId = string.IsNullOrWhiteSpace(dto.UnitId) ? null : dto.UnitId;

        await _foodRepo.Update(entity);

        return FoodDtoAssembler.Assemble(entity);
    }
}
