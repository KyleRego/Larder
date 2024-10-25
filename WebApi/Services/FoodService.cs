using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

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

public class FoodService(IServiceProviderWrapper serviceProvider,
                                                IFoodRepository repo,
                                        IConsumedFoodRepository conFoodRepo)
                        : AppServiceBase(serviceProvider), IFoodService
{
    private readonly IFoodRepository _repository = repo;
    private readonly IConsumedFoodRepository _conFoodRepo = conFoodRepo;

    private static void UpdateFoodTotals(Food food)
    {
        food.TotalCalories = food.Calories * food.Servings;
        food.TotalGramsProtein = food.GramsProtein * food.Servings;
    }

    public async Task<FoodDto> CreateFood(FoodDto dto)
    {
        Item item = new()
        {
            UserId = CurrentUserId(),
            Name = dto.Name,
            Description = dto.Description
        };

        Food food = new()
        {
            Item = item,
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
            GramsTotalSugars = dto.GramsTotalSugars
        };
        item.Food = food;

        UpdateFoodTotals(food);

        await _repository.Insert(item);

        return FoodDto.FromEntity(item);
    }

    public async Task<FoodDto> UpdateFood(FoodDto dto)
    {
        if (dto.Id == null) throw new ApplicationException("food id was missing");

        Item item = await _repository.Get(dto.Id)
                ?? throw new ApplicationException("food was not found");
        ArgumentNullException.ThrowIfNull(item.Food);

        await ThrowIfUserCannotAccess(item);

        item.Name = dto.Name;
        item.Description = dto.Description;
        item.Food.Calories = dto.Calories;
        item.Food.Servings = dto.Servings;
        item.Food.GramsProtein = dto.GramsProtein;
        item.Food.GramsTotalFat = dto.GramsTotalFat;
        item.Food.GramsSaturatedFat = dto.GramsSaturatedFat;
        item.Food.GramsTransFat = dto.GramsTransFat;
        item.Food.MilligramsCholesterol = dto.MilligramsCholesterol;
        item.Food.MilligramsSodium = dto.MilligramsSodium;
        item.Food.GramsTotalCarbs = dto.GramsTotalCarbs;
        item.Food.GramsDietaryFiber = dto.GramsDietaryFiber;
        item.Food.GramsTotalSugars = dto.GramsTotalSugars;

        UpdateFoodTotals(item.Food);

        await _repository.Update(item);

        return FoodDto.FromEntity(item);
    }

    public async Task<FoodDto?> GetFood(string id)
    {
        Item? item = await _repository.Get(id);
        
        if (item == null) return null;

        await ThrowIfUserCannotAccess(item);

        return FoodDto.FromEntity(item);
    }

    public async Task<List<FoodDto>> GetFoods(FoodSortOptions sortBy,
                                                        string? search)
    {
        List<Item> foodItems = await _repository.GetAllForUser(CurrentUserId(),
                                                            sortBy,
                                                            search);

        List<FoodDto> result = [];

        foreach (Item foodItem in foodItems)
        {
            result.Add(FoodDto.FromEntity(foodItem));
        }

        return result;
    }

    public async Task<FoodDto> UpdateServings(FoodServingsDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.FoodId);

        Item foodItem = await _repository.Get(dto.FoodId)
                ?? throw new ApplicationException("food not found");
        ArgumentNullException.ThrowIfNull(foodItem.Food);

        await ThrowIfUserCannotAccess(foodItem);

        foodItem.Food.Servings = dto.Servings;

        UpdateFoodTotals(foodItem.Food);

        await _repository.Update(foodItem);

        return FoodDto.FromEntity(foodItem);
    }

    public async Task DeleteFood(string id)
    {
        Item item = await _repository.Get(id)
            ?? throw new ApplicationException("food not found");

        await ThrowIfUserCannotAccess(item);

        await _repository.Delete(item);
    }

    public async Task<(FoodDto, ConsumedFoodDto)> EatFood(FoodServingsDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.FoodId);

        if (dto.Servings < 1)
            throw new ApplicationException("food servings must be >= 1");

        Item item = await _repository.Get(dto.FoodId)
            ?? throw new ApplicationException("food not found");
        ArgumentNullException.ThrowIfNull(item.Food);

        await ThrowIfUserCannotAccess(item);

        if (dto.Servings > item.Food.Servings)
            throw new ApplicationException("there are not that many servings");

        ConsumedFood consumedFood = new()
        {
            UserId = CurrentUserId(),
            FoodName = item.Name,
            DateConsumed = DateOnly.FromDateTime(DateTime.Now),
            CaloriesConsumed = item.Food.Calories * dto.Servings,
            GramsProteinConsumed = item.Food.GramsProtein * dto.Servings
        };

        item.Food.Servings -= dto.Servings;
        
        UpdateFoodTotals(item.Food);

        await _repository.Update(item);
        await _conFoodRepo.Insert(consumedFood);

        return (FoodDto.FromEntity(item), ConsumedFoodDto.FromEntity(consumedFood));
    }
}
