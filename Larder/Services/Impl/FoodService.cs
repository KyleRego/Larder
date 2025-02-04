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

    public async Task<ItemDto> EatFood(EatFoodDto dto)
    {
        Item foodItem = await _foodData.Get(CurrentUserId(), dto.ItemId)
            ?? throw new ApplicationException(
                $"Food with id {dto.ItemId} not found");

        Nutrition nutrition = foodItem.Nutrition
            ?? throw new ApplicationException(
                $"Food with id {dto.ItemId} has no nutrition component");

        Quantity foodQuantity = foodItem.Quantity;
        Quantity quantityEaten = Quantity.FromDto(dto.QuantityEaten);

        Quantity quantityLeft = await _quantityMathService.Subtract(foodQuantity, quantityEaten);
        foodItem.Quantity = quantityLeft;

        Item updatedItem = await _foodData.Update(foodItem);

        return ItemDto.FromEntity(updatedItem);
    }
}
