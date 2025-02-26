using Larder.Dtos;
using Larder.Models;
using Larder.Models.Builders;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class ItemService(IServiceProviderWrapper serviceProvider,
                                        IItemRepository itemData)
        : CrudServiceBase<ItemDto, Item>(serviceProvider, itemData),
        IItemService
{
    private readonly IItemRepository _itemData = itemData;

    public async Task<ItemDto> FindOrCreate(string name)
    {
        Item item = await _itemData.FindOrCreate(CurrentUserId(), name);
        return MapToDto(item);
    }

    public async Task<List<ItemDto>> GetItems(ItemSortOptions sortBy,
                                                        string? search)
    {
        string userId = CurrentUserId();

        List<Item> items =
                await _itemData.GetAll(userId, sortBy, search);

        return items.Select(ItemDto.FromEntity).ToList();
    }

    protected override ItemDto MapToDto(Item item)
    {
        ItemDto itemDto = new()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Nutrition = (item.Nutrition != null)
                        ? NutritionDto.FromEntity(item.Nutrition)
                        : null,

            Quantity = (item.Quantity != null)
                ? QuantityDto.FromEntity(item.Quantity)
                : null
        };

        return itemDto;
    }

    protected override Task<Item> MapToEntity(ItemDto dto)
    {
        ItemBuilder builder = new(CurrentUserId(), dto.Name, dto.Description);

        if (dto.Quantity != null)
            builder = builder.WithQuantity(dto.Quantity);

        var nutrition = dto.Nutrition;

        if (nutrition != null)
        {
            builder.WithNutrition(new NutritionBuilder()
                .WithCalories(nutrition.Calories)
                .WithCholesterol(nutrition.MilligramsCholesterol)
                .WithDietaryFiber(nutrition.GramsDietaryFiber)
                .WithProtein(nutrition.GramsProtein)
                .WithSaturatedFat(nutrition.GramsSaturatedFat)
                .WithSodium(nutrition.MilligramsSodium)
                .WithTotalCarbs(nutrition.GramsTotalCarbs)
                .WithTransFat(nutrition.GramsTransFat)
                .WithServingSize(nutrition.ServingSize)
            );
        }

        return Task.FromResult(builder.Build());
    }
}
