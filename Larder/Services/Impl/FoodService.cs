using Larder.Dtos;
using Larder.Models;
using Larder.Models.ItemComponents;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class FoodService(  IServiceProviderWrapper serviceProvider,
                                IQuantityMathService quantityMathService,
                                IFoodRepository foodRepository)
                        : AppServiceBase(serviceProvider), IFoodService
{
    private readonly IFoodRepository _foodData = foodRepository;
    private readonly IQuantityMathService _quantityMathService
                                            = quantityMathService;

    public async Task<List<ItemDto>> GetFoods(FoodSortOptions sortBy,
                                                        string? search)
    {
        return [.. (await _foodData.GetAll(CurrentUserId(), sortBy, search))
                                    .Select(ItemDto.FromEntity)];
    }

    public async Task EatFood(FoodServingsDto dto)
    {
        Item foodItem = await _foodData.Get(CurrentUserId(), dto.FoodId)
            ?? throw new ApplicationException(
                $"Food with id {dto.FoodId} not found");

        Nutrition nutrition = foodItem.Nutrition
            ?? throw new ApplicationException(
                $"Food with id {dto.FoodId} has no nutrition component");

        Quantity foodQuantity = foodItem.Quantity;
        Quantity quantityEaten = Quantity.FromDto(dto.QuantityEaten);

        Quantity quantityLeft = await _quantityMathService.Subtract(foodQuantity, quantityEaten);
        foodItem.Quantity = quantityLeft;

        await _foodData.Update(foodItem);
    }
}
