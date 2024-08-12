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

    public Task DeleteFood(string id);
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
            Protein = Quantity.FromDto(dto.Protein),
            TotalFat = Quantity.FromDto(dto.TotalFat),
            SaturatedFat = Quantity.FromDto(dto.SaturatedFat),
            TransFat = Quantity.FromDto(dto.TransFat),
            Cholesterol = Quantity.FromDto(dto.Cholesterol),
            Sodium = Quantity.FromDto(dto.Sodium),
            TotalCarbs = Quantity.FromDto(dto.TotalCarbs),
            DietaryFiber = Quantity.FromDto(dto.DietaryFiber),
            TotalSugars = Quantity.FromDto(dto.TotalSugars),
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
        entity.Protein = Quantity.FromDto(dto.Protein);
        entity.TotalFat = Quantity.FromDto(dto.TotalFat);
        entity.SaturatedFat = Quantity.FromDto(dto.SaturatedFat);
        entity.TransFat = Quantity.FromDto(dto.TransFat);
        entity.Cholesterol = Quantity.FromDto(dto.Cholesterol);
        entity.Sodium = Quantity.FromDto(dto.Sodium);
        entity.TotalCarbs = Quantity.FromDto(dto.TotalCarbs);
        entity.DietaryFiber = Quantity.FromDto(dto.DietaryFiber);
        entity.TotalSugars = Quantity.FromDto(dto.TotalSugars);

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

        await _foodRepo.Update(entity);

        return FoodDtoAssembler.Assemble(entity);
    }

    public async Task DeleteFood(string id)
    {
        Food entity = await _foodRepo.Get(id) ?? throw new ApplicationException("food not found");

        await _foodRepo.Delete(entity);
    }
}
