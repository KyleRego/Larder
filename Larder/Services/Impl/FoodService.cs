using Larder.Dtos;
using Larder.Models;
using Larder.Models.ItemComponents;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class FoodService(  IServiceProviderWrapper serviceProvider,
                                IQuantityService quantityMathService,
                                IFoodRepository foodRepository)
                        : AppServiceBase(serviceProvider), IFoodService
{
    private readonly IFoodRepository _foodData = foodRepository;
    private readonly IQuantityService _quantityMathService
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
                $"Food with ID {dto.ItemId} has no Nutrition component");

        QuantityDto foodQuantity = QuantityDto.FromEntity(foodItem.Quantity);

        QuantityDto quantityLeft = await _quantityMathService
                    .SubtractUpToZero(foodQuantity, dto.QuantityEaten);
        foodItem.Quantity = Quantity.FromDto(quantityLeft);

        QuantityDto quantityEaten =
            (quantityLeft.Amount == 0) ? foodQuantity : dto.QuantityEaten;

        // TODO: if nutrition serving size is missing unit, catch
        // the error and rethrow with a more helpful message here
        double servingsConsumed = await _quantityMathService.Divide(
                    quantityEaten, QuantityDto.FromEntity(nutrition.ServingSize));

        Item updatedFood = await _foodData.Update(foodItem);

        Item consumedFoodResult = new(CurrentUserId(), foodItem.Name)
        {
            Quantity = Quantity.FromDto(quantityEaten)
        };
        Nutrition consumedNutrition = new()
        {
            Item = consumedFoodResult,
            ServingSize = Quantity.FromDto(quantityEaten),
            Calories = nutrition.Calories * servingsConsumed,
            GramsProtein = nutrition.GramsProtein * servingsConsumed,
            GramsDietaryFiber = nutrition.GramsDietaryFiber * servingsConsumed,
            GramsSaturatedFat = nutrition.GramsSaturatedFat * servingsConsumed,
            GramsTotalCarbs = nutrition.GramsTotalCarbs * servingsConsumed,
            GramsTotalFat = nutrition.GramsTotalFat * servingsConsumed,
            GramsTotalSugars = nutrition.GramsTotalSugars * servingsConsumed,
            GramsTransFat = nutrition.GramsTransFat * servingsConsumed,
            MilligramsCholesterol = nutrition.MilligramsCholesterol * servingsConsumed,
            MilligramsSodium = nutrition.MilligramsSodium * servingsConsumed
        };
        consumedFoodResult.Nutrition = consumedNutrition;
        ConsumedTime consumedTime = new()
        {
            Item = consumedFoodResult,
            ConsumedAt = DateTimeOffset.Now 
        };
        consumedFoodResult.ConsumedTime = consumedTime;

        await _foodData.Insert(consumedFoodResult);

        return (ItemDto.FromEntity(updatedFood),
                    ItemDto.FromEntity(consumedFoodResult));
    }
 
    public async Task<List<ItemDto>> ConsumedFoods(DateTime day)
    {
        List<Item> items = await _foodData.GetConsumedFoods(CurrentUserId(), day);

        return [.. items.Select(ItemDto.FromEntity)];
    }
}
