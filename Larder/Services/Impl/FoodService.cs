using Larder.Dtos;
using Larder.Models;
using Larder.Models.ItemComponents;
using Larder.Repository;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class FoodService(   IServiceProviderWrapper serviceProvider,
                            IFoodRepository foodRepository,
                            IConsumedFoodRepository consumedFoodRepository)
                                : AppServiceBase(serviceProvider), IFoodService
{
    private readonly IFoodRepository _foodData = foodRepository;
    private readonly IConsumedFoodRepository _consumedFoodData
                                                    = consumedFoodRepository;

    private static void ValidateFoodItem(Item item)
    {
        if (item.Food == null) throw new ApplicationException(
            "Item is not a food"
        );
    }

    public async Task<ItemDto?> GetFood(string id)
    {
        Item? item = await _foodData.Get(CurrentUserId(), id);

        if (item == null) return null;
        ValidateFoodItem(item);

        return ItemDto.FromEntity(item);
    }

    public async Task<List<ItemDto>> GetFoods(FoodSortOptions sortBy,
                                                        string? search)
    {
        return (await _foodData.GetAll(CurrentUserId(), sortBy, search))
                                .Select(ItemDto.FromEntity).ToList();
    }

    public async Task<FoodDto> UpdateServings(FoodServingsDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.FoodId);

        Item foodItem = await _foodData.Get(CurrentUserId(), dto.FoodId)
                ?? throw new ApplicationException("food not found");
        Food? food = foodItem.Food;
        ArgumentNullException.ThrowIfNull(food);

        food.Servings = dto.Servings;

        food.UpdateTotals();

        await _foodData.Update(foodItem);

        return FoodDto.FromEntity(foodItem);
    }

    public async Task<(FoodDto, ConsumedFoodDto)> EatFood(FoodServingsDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.FoodId);

        if (dto.Servings < 1)
            throw new ApplicationException("food servings must be >= 1");

        Item foodItem = await _foodData.Get(CurrentUserId(), dto.FoodId)
            ?? throw new ApplicationException("food not found");
        Food? food = foodItem.Food;
        ArgumentNullException.ThrowIfNull(food);

        if (dto.Servings > food.Servings)
            throw new ApplicationException("there are not that many servings");

        ConsumedFood consumedFood = new(CurrentUserId())
        {
            FoodName = foodItem.Name,
            DateConsumed = DateOnly.FromDateTime(DateTime.Now),
            CaloriesConsumed = food.Calories * dto.Servings,
            GramsProteinConsumed = food.GramsProtein * dto.Servings
        };

        food.Servings -= dto.Servings;
        
        food.UpdateTotals();

        await _foodData.Update(foodItem);
        await _consumedFoodData.Insert(consumedFood);

        return (FoodDto.FromEntity(foodItem), ConsumedFoodDto.FromEntity(consumedFood));
    }
}
