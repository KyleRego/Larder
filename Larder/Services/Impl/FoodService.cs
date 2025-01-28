using Larder.Dtos;
using Larder.Models;
using Larder.Models.ItemComponents;
using Larder.Repository;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class FoodService(   IServiceProviderWrapper serviceProvider,
                            IFoodRepository foodRepository)
                                : AppServiceBase(serviceProvider), IFoodService
{
    private readonly IFoodRepository _foodData = foodRepository;

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
        Nutrition? food = foodItem.Food;
        ArgumentNullException.ThrowIfNull(food);

        await _foodData.Update(foodItem);

        return FoodDto.FromEntity(foodItem);
    }

    public async Task<FoodDto> EatFood(FoodServingsDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.FoodId);

        if (dto.Servings < 1)
            throw new ApplicationException("food servings must be >= 1");

        Item foodItem = await _foodData.Get(CurrentUserId(), dto.FoodId)
            ?? throw new ApplicationException("food not found");
        Nutrition? food = foodItem.Food;
        ArgumentNullException.ThrowIfNull(food);

        await _foodData.Update(foodItem);

        return FoodDto.FromEntity(food);
    }
}
