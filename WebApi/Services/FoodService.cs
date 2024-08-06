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
            Calories = dto.Calories
        };

        if (dto.Quantity != null)
        {
            entity.Quantity = new()
            {
                Amount = dto.Quantity.Amount,
                UnitId = dto.Quantity.UnitId
            };
        }

        await _foodRepo.Insert(entity);

        return dto;
    }

    public async Task<FoodDto> UpdateFood(FoodDto dto)
    {
        if (dto.Id == null) throw new ApplicationException("food id was missing");

        Food? entity = await _foodRepo.Get(dto.Id);

        if (entity == null) throw new ApplicationException("food was not found");

        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.Calories = dto.Calories;

        if (dto.Quantity != null)
        {
            if (entity.Quantity == null)
            {
                entity.Quantity = new()
                {
                    Amount = dto.Quantity.Amount,
                    UnitId = dto.Quantity.UnitId
                };
            }
            else
            {
                entity.Quantity.Amount = dto.Quantity.Amount;
                entity.Quantity.UnitId = dto.Quantity.UnitId;
            }
        }

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

    public async Task<FoodDto> UpdateQuantity(QuantityDto quantity)
    {
        ArgumentNullException.ThrowIfNull(quantity.Id);

        Food entity = await _foodRepo.Get(quantity.Id) ?? throw new ApplicationException("food not found");

        if (entity.Quantity == null)
        {
            entity.Quantity = new()
            {
                Amount = quantity.Amount,
                UnitId = quantity.UnitId
            };
        }
        else
        {
            entity.Quantity.Amount = quantity.Amount;
            entity.Quantity.UnitId = quantity.UnitId;
        }

        await _foodRepo.Update(entity);

        return FoodDtoAssembler.Assemble(entity);
    }
}
