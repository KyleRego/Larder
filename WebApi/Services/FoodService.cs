using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Larder.Services;

public interface IFoodService
{
    public Task<FoodDto?> GetFood(string id);

    public Task<List<FoodDto>> GetFoods(FoodSortOptions sortOrder,
                                                    string? search);

    public Task<FoodDto> CreateFood(FoodDto food);

    public Task<FoodDto> UpdateFood(FoodDto food);

    public Task<FoodDto> UpdateServings(FoodServingsDto dto);

    public Task<(FoodDto, ConsumedFoodDto)> EatFood(FoodServingsDto dto);

    public Task DeleteFood(string id);
}

public class FoodService(IFoodRepository repository,
                            IConsumedFoodRepository conFoodRepo,
                            IHttpContextAccessor httpConAcsr,
                            IAuthorizationService authService)
        : ApplicationServiceBase(httpConAcsr, authService), IFoodService
{
    private readonly IFoodRepository _repository = repository;
    private readonly IConsumedFoodRepository _conFoodRepo = conFoodRepo;

    public async Task<FoodDto> CreateFood(FoodDto dto)
    {
        double servings = dto.Servings;
        double calories = dto.Calories;
        double proteins = dto.GramsProtein;

        Food entity = new()
        {
            UserId = CurrentUserId(),
            Name = dto.Name,
            Description = dto.Description,
            Calories = calories,
            Servings = servings,
            GramsProtein = dto.GramsProtein,
            GramsTotalFat = dto.GramsTotalFat,
            GramsSaturatedFat = dto.GramsSaturatedFat,
            GramsTransFat = dto.GramsTransFat,
            MilligramsCholesterol = dto.MilligramsCholesterol,
            MilligramsSodium = dto.MilligramsSodium,
            GramsTotalCarbs = dto.GramsTotalCarbs,
            GramsDietaryFiber = dto.GramsDietaryFiber,
            GramsTotalSugars = dto.GramsTotalSugars,

            TotalCalories = calories * servings,
            TotalGramsProtein = proteins * servings
        };

        await _repository.Insert(entity);

        return FoodDto.FromEntity(entity);
    }

    public async Task<FoodDto> UpdateFood(FoodDto dto)
    {
        if (dto.Id == null) throw new ApplicationException("food id was missing");

        Food entity = await _repository.Get(dto.Id)
                ?? throw new ApplicationException("food was not found");

        await ThrowIfUserCannotAccess(entity);

        double servings = dto.Servings;
        double proteins = dto.GramsProtein;
        double calories = dto.Calories;

        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.Calories = calories;
        entity.Servings = servings;
        entity.GramsProtein = dto.GramsProtein;
        entity.GramsTotalFat = dto.GramsTotalFat;
        entity.GramsSaturatedFat = dto.GramsSaturatedFat;
        entity.GramsTransFat = dto.GramsTransFat;
        entity.MilligramsCholesterol = dto.MilligramsCholesterol;
        entity.MilligramsSodium = dto.MilligramsSodium;
        entity.GramsTotalCarbs = dto.GramsTotalCarbs;
        entity.GramsDietaryFiber = dto.GramsDietaryFiber;
        entity.GramsTotalSugars = dto.GramsTotalSugars;

        entity.TotalCalories = servings * calories;
        entity.TotalGramsProtein = servings * proteins;

        await _repository.Update(entity);

        return FoodDto.FromEntity(entity);
    }

    public async Task<FoodDto?> GetFood(string id)
    {
        Food? entity = await _repository.Get(id);
        
        if (entity == null) return null;

        await ThrowIfUserCannotAccess(entity);

        return FoodDto.FromEntity(entity);
    }

    public async Task<List<FoodDto>> GetFoods(FoodSortOptions sortBy,
                                                        string? search)
    {
        List<Food> foods = await _repository.GetAllForUser(CurrentUserId(),
                                                            sortBy,
                                                            search);

        List<FoodDto> result = [];

        foreach (Food food in foods)
        {
            result.Add(FoodDto.FromEntity(food));
        }

        return result;
    }

    public async Task<FoodDto> UpdateServings(FoodServingsDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.FoodId);

        Food entity = await _repository.Get(dto.FoodId)
                ?? throw new ApplicationException("food not found");

        await ThrowIfUserCannotAccess(entity);

        entity.Servings = dto.Servings;

        await _repository.Update(entity);

        return FoodDto.FromEntity(entity);
    }

    public async Task DeleteFood(string id)
    {
        Food entity = await _repository.Get(id)
                ?? throw new ApplicationException("food not found");

        await ThrowIfUserCannotAccess(entity);

        await _repository.Delete(entity);
    }

    public async Task<(FoodDto, ConsumedFoodDto)> EatFood(FoodServingsDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.FoodId);

        if (dto.Servings < 1)
            throw new ApplicationException("food servings must be >= 1");

        Food entity = await _repository.Get(dto.FoodId)
            ?? throw new ApplicationException("food not found");

        await ThrowIfUserCannotAccess(entity);

        if (dto.Servings > entity.Servings)
            throw new ApplicationException("there are not that many servings");

        ConsumedFood consumedFood = new()
        {
            UserId = CurrentUserId(),
            FoodName = entity.Name,
            DateConsumed = DateOnly.FromDateTime(DateTime.Now),
            CaloriesConsumed = entity.Calories * dto.Servings,
            GramsProteinConsumed = entity.GramsProtein * dto.Servings
        };

        entity.Servings -= dto.Servings;
        entity.TotalCalories = entity.Servings * entity.Calories;
        entity.TotalGramsProtein = entity.Servings * entity.GramsProtein;

        await _repository.Update(entity);
        await _conFoodRepo.Insert(consumedFood);

        return (FoodDto.FromEntity(entity), ConsumedFoodDto.FromEntity(consumedFood));
    }
}
