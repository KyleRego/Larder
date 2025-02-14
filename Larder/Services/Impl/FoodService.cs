using Larder.Dtos;
using Larder.Models;
using Larder.Models.Builders;
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
        List<Item> foodItems =
            await _foodData.GetAll(CurrentUserId(), sortBy, search);

        return [.. foodItems.Select(ItemDto.FromEntity)];
    }

    public async Task<List<ItemDto>> GetConsumedFoods(DateTime day)
    {
        List<Item> eatenFoodItems
            = await _foodData.GetConsumedFoods(CurrentUserId(), day);

        return [.. eatenFoodItems.Select(ItemDto.FromEntity)];
    }

    public async Task<(ItemDto leftOverFood, ItemDto eatenFood)> EatFood(EatFoodDto dto)
    {
        Item foodItem = await _foodData.Get(CurrentUserId(), dto.ItemId)
            ?? throw new ApplicationException($"Food with ID {dto.ItemId} not found");

        QuantityDto initialQuantity = (QuantityDto)foodItem.Quantity;

        Nutrition nutrition = foodItem.Nutrition
            ?? throw new ApplicationException(
                $"Food with ID {dto.ItemId} has no Nutrition component");

        QuantityDto quantityLeftOver = await _quantityMathService
            .SubtractUpToZero(initialQuantity, dto.QuantityEaten);

        QuantityDto quantityEaten =
            (quantityLeftOver.Amount == 0) ? initialQuantity : dto.QuantityEaten;

        double servingsConsumed;
        try
        {
            servingsConsumed = await _quantityMathService.Divide(
                    quantityEaten, QuantityDto.FromEntity(nutrition.ServingSize));
        }
        catch (ApplicationException e)
        {
            throw new ApplicationException(
                $"{e.Message} - Does the Nutrition have a serving size?");
        }

        foodItem.Quantity = Quantity.FromDto(quantityLeftOver);

        Item leftOverFood = await _foodData.Update(foodItem);

        Item eatenFood = new ItemBuilder(CurrentUserId(), leftOverFood.Name)
                            .WithQuantity(Quantity.FromDto(quantityEaten))
                            .WithNutrition(new NutritionBuilder()
                                .WithServingSize(Quantity.FromDto(quantityEaten))
                                .WithCalories(nutrition.Calories * servingsConsumed)
                                .WithProtein(nutrition.GramsProtein * servingsConsumed)
                                .WithDietaryFiber(nutrition.GramsDietaryFiber * servingsConsumed)
                                .WithSaturatedFat(nutrition.GramsSaturatedFat * servingsConsumed)
                                .WithTotalCarbs(nutrition.GramsTotalCarbs * servingsConsumed)
                                .WithTotalFat(nutrition.GramsTotalFat * servingsConsumed)
                                .WithTotalSugars(nutrition.GramsTotalSugars * servingsConsumed)
                                .WithTransFat(nutrition.GramsTransFat * servingsConsumed)
                                .WithCholesterol(nutrition.MilligramsCholesterol * servingsConsumed)
                                .WithSodium(nutrition.MilligramsSodium * servingsConsumed))
                            .Build();
        ConsumedTime consumedTime = new()
        {
            Item = eatenFood,
            ConsumedAt = DateTimeOffset.Now 
        };
        eatenFood.ConsumedTime = consumedTime;

        await _foodData.Insert(eatenFood);

        return (ItemDto.FromEntity(leftOverFood), ItemDto.FromEntity(eatenFood));
    }
}
