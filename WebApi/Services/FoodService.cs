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

    public Task<FoodDto> UpdateServings(FoodServingsDto dto);

    public Task<FoodDto> ConsumeServings(FoodServingsDto dto);

    public Task DeleteFood(string id);
}

public class FoodService(IFoodRepository foodRepo,
                            IConsumedFoodRepository consumedFoodRepository) : IFoodService
{
    private readonly IFoodRepository _foodRepo = foodRepo;
    private readonly IConsumedFoodRepository _consumedFoodRepo = consumedFoodRepository;

    public async Task<FoodDto> CreateFood(FoodDto dto)
    {
        Food entity = new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Calories = dto.Calories,
            Servings = dto.Servings,
            GramsProtein = dto.GramsProtein,
            GramsTotalFat = dto.GramsTotalFat,
            GramsSaturatedFat = dto.GramsSaturatedFat,
            GramsTransFat = dto.GramsTransFat,
            MilligramsCholesterol = dto.MilligramsCholesterol,
            MilligramsSodium = dto.MilligramsSodium,
            GramsTotalCarbs = dto.GramsTotalCarbs,
            GramsDietaryFiber = dto.GramsDietaryFiber,
            GramsTotalSugars = dto.GramsTotalSugars,
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
        entity.Servings = dto.Servings;
        entity.GramsProtein = dto.GramsProtein;
        entity.GramsTotalFat = dto.GramsTotalFat;
        entity.GramsSaturatedFat = dto.GramsSaturatedFat;
        entity.GramsTransFat = dto.GramsTransFat;
        entity.MilligramsCholesterol = dto.MilligramsCholesterol;
        entity.MilligramsSodium = dto.MilligramsSodium;
        entity.GramsTotalCarbs = dto.GramsTotalCarbs;
        entity.GramsDietaryFiber = dto.GramsDietaryFiber;
        entity.GramsTotalSugars = dto.GramsTotalSugars;

        await _foodRepo.Update(entity);

        return dto;
    }

    public async Task<FoodDto?> GetFood(string id)
    {
        Food? entity = await _foodRepo.Get(id);

        if (entity == null) return null;

        return FoodDto.FromEntity(entity);
    }

    public async Task<List<FoodDto>> GetFoods(FoodSortOptions sortBy, string? search)
    {
        List<Food> foods = await _foodRepo.GetAll(sortBy, search);

        List<FoodDto> foodDtos = [];

        foreach (Food food in foods)
        {
            foodDtos.Add(FoodDto.FromEntity(food));
        }

        return foodDtos;
    }

    public async Task<FoodDto> UpdateServings(FoodServingsDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.FoodId);

        Food entity = await _foodRepo.Get(dto.FoodId) ?? throw new ApplicationException("food not found");

        entity.Servings = dto.Servings;

        await _foodRepo.Update(entity);

        return FoodDto.FromEntity(entity);
    }

    public async Task DeleteFood(string id)
    {
        Food entity = await _foodRepo.Get(id) ?? throw new ApplicationException("food not found");

        await _foodRepo.Delete(entity);
    }

    public async Task<FoodDto> ConsumeServings(FoodServingsDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.FoodId);
        if (dto.Servings < 1) throw new ApplicationException("servings must be >= 1 when consuming a food");

        Food entity = await _foodRepo.Get(dto.FoodId) ?? throw new ApplicationException("food not found");

        entity.Servings -= dto.Servings;

        DateTime dt = DateTime.Now;

        ConsumedFood consumedFood = new()
        {
            FoodName = entity.Name,
            DateTimeConsumed = dt,
            DateConsumed = DateOnly.FromDateTime(dt),
            ServingsConsumed = dto.Servings,
            CaloriesConsumed = entity.Calories * dto.Servings,
            ProteinConsumed = entity.GramsProtein * dto.Servings
        };

        await _foodRepo.Update(entity);

        await _consumedFoodRepo.Insert(consumedFood);

        return FoodDto.FromEntity(entity);
    }
}
