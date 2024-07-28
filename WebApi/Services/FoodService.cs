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

    public async Task<FoodDto> CreateFood(FoodDto food)
    {
        Food entity = new()
        {
            Name = food.Name,
            Description = food.Description,
            Quantity = food.Quantity,
            Calories = food.Calories
        };

        await _foodRepo.Insert(entity);

        return food;
    }

    public async Task<FoodDto> UpdateFood(FoodDto food)
    {
        if (food.Id == null) throw new ApplicationException("food id was missing");

        Food? entity = await _foodRepo.Get(food.Id);

        if (entity == null ) throw new ApplicationException("food was not found");

        entity.Name = food.Name;
        entity.Description = food.Description;
        entity.Quantity = food.Quantity;
        entity.Calories = food.Calories;

        await _foodRepo.Update(entity);

        return food;
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
        Food? entity = await _foodRepo.Get(quantity.Id);

        if (entity == null)
        {
            throw new ApplicationException("Food was not found");
        }

        entity.Quantity = (int)quantity.Quantity;

        await _foodRepo.Update(entity);

        return FoodDtoAssembler.Assemble(entity);
    }
}