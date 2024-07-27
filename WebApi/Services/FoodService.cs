using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IFoodService
{
    public Task<List<FoodDto>> GetFoods(FoodSortOptions sortOrder, string? search);
}

public class FoodService(IFoodRepository foodRepo) : IFoodService
{
    private readonly IFoodRepository _foodRepo = foodRepo;

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
}