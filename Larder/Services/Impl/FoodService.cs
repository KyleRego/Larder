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

    public async Task<(ItemDto, ItemDto)> EatFood(EatFoodDto dto)
    {
        Item foodItem = await _foodData.Get(CurrentUserId(), dto.ItemId)
            ?? throw new ApplicationException(
                $"Food with id {dto.ItemId} not found");

        Nutrition nutrition = foodItem.Nutrition
            ?? throw new ApplicationException(
                $"Food with id {dto.ItemId} has no nutrition component");

        Quantity foodQuantity = foodItem.Quantity;
        Quantity eatFoodQuantity = Quantity.FromDto(dto.QuantityEaten);

        QuantityDto quantityLeft = await _quantityMathService
                    .SubtractUpToZero(foodQuantity, eatFoodQuantity);
        foodItem.Quantity = Quantity.FromDto(quantityLeft);

        Quantity quantityEaten;
        if (quantityLeft.Amount == 0)
        {
            quantityEaten = foodQuantity;
        }
        else
        {
            quantityEaten = eatFoodQuantity;
        }

        Item updatedFood = await _foodData.Update(foodItem);

        Item eatenFoodResult = new(CurrentUserId(),
                    $"{foodItem.Name} - Eaten")
        {
            Quantity = quantityEaten
        };
        ConsumedTime consumedTime = new()
        {
            ConsumedAt = DateTimeOffset.Now,
            Item = eatenFoodResult
        };
        eatenFoodResult.ConsumedTime = consumedTime;

        await _foodData.Insert(eatenFoodResult);

        return (ItemDto.FromEntity(updatedFood),
                    ItemDto.FromEntity(eatenFoodResult));
    }

    public async Task<List<ItemDto>> ConsumedFoods(DateTime day)
    {
        List<Item> items = await _foodData.GetConsumedFoods(CurrentUserId(), day);

        return [.. items.Select(ItemDto.FromEntity)];
    }
}
