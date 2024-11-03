using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IFoodService
{
    public Task<FoodDto?> GetFood(string id);

    public Task<List<FoodDto>> GetFoods(FoodSortOptions sortOrder,
                                                    string? search);

    public Task<FoodDto> UpdateServings(FoodServingsDto dto);

    public Task<(FoodDto, ConsumedFoodDto)> EatFood(FoodServingsDto dto);
}

public class FoodService(   IServiceProviderWrapper serviceProvider,
                            IFoodRepository foodRepository,
                            IConsumedFoodRepository consumedFoodRepository)
                                : AppServiceBase(serviceProvider), IFoodService
{
    private readonly IFoodRepository _foodData = foodRepository;
    private readonly IConsumedFoodRepository _consumedFoodData
                                                    = consumedFoodRepository;

    public async Task<FoodDto?> GetFood(string id)
    {
        Item? item = await _foodData.Get(CurrentUserId(), id);
        
        if (item == null) return null;

        return FoodDto.FromEntity(item);
    }

    public async Task<List<FoodDto>> GetFoods(FoodSortOptions sortBy,
                                                        string? search)
    {
        List<Item> foodItems = await _foodData.GetAll(CurrentUserId(),
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

        Item foodItem = await _foodData.Get(CurrentUserId(), dto.FoodId)
                ?? throw new ApplicationException("food not found");
        ArgumentNullException.ThrowIfNull(foodItem.Food);

        foodItem.Food.Servings = dto.Servings;

        foodItem.Food.UpdateTotals();

        await _foodData.Update(foodItem);

        return FoodDto.FromEntity(foodItem);
    }

    public async Task<(FoodDto, ConsumedFoodDto)> EatFood(FoodServingsDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.FoodId);

        if (dto.Servings < 1)
            throw new ApplicationException("food servings must be >= 1");

        Item item = await _foodData.Get(CurrentUserId(), dto.FoodId)
            ?? throw new ApplicationException("food not found");
        ArgumentNullException.ThrowIfNull(item.Food);

        if (dto.Servings > item.Food.Servings)
            throw new ApplicationException("there are not that many servings");

        ConsumedFood consumedFood = new(CurrentUserId())
        {
            FoodName = item.Name,
            DateConsumed = DateOnly.FromDateTime(DateTime.Now),
            CaloriesConsumed = item.Food.Calories * dto.Servings,
            GramsProteinConsumed = item.Food.GramsProtein * dto.Servings
        };

        item.Food.Servings -= dto.Servings;
        
        item.Food.UpdateTotals();

        await _foodData.Update(item);
        await _consumedFoodData.Insert(consumedFood);

        return (FoodDto.FromEntity(item), ConsumedFoodDto.FromEntity(consumedFood));
    }
}
