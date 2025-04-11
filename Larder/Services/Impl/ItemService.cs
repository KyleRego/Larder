using Larder.Dtos;
using Larder.Models;
using Larder.Models.Builders;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;
using Larder.Services.Interface;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

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

    public Task<List<ItemDto>> GetItems()
    {
        return GetItems(ItemSortOptions.AnyOrder, null);
    }

    public async Task<List<ItemDto>> GetItems(ItemSortOptions sortBy,
                                                        string? search)
    {
        string userId = CurrentUserId();

        List<Item> items =
                await _itemData.GetAll(userId, sortBy, search);

        return items.Select(ItemDto.FromEntity).ToList();
    }

    // To prevent the recursion becoming a problem if there was a cycle
    // this could delegate to MapToDtoSafe(Item item, HashSet<string> visited)
    // see https://chatgpt.com/c/67f57e7b-a978-8002-9974-621fb5315262 (if Kyle)
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
                : null,

            IsContainer = item.Container != null,

            ContainedItems = item.Container != null ?
                [.. item.Container.Items.Select(MapToDto)] : [],
        };

        return itemDto;
    }

    protected async override Task<Item> MapToEntity(ItemDto dto)
    {
        string userId = CurrentUserId();

        Item? existing = (dto.Id != null) ? await _itemData.Get(userId, dto.Id) : null;

        ItemBuilder builder = new(CurrentUserId(), dto.Name, dto.Description);

        if (dto.Id != null)
            builder = builder.WithId(dto.Id);

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

        if (dto.IsContainer)
            AddContainer(builder, existing);

        return builder.Build();
    }

    private static void AddContainer(ItemBuilder builder, Item? existing)
    {
        if (existing?.Container == null)
        {
            builder.WithContainer();
        }
        else if (existing?.Container != null)
        {
            builder.WithContainedItems(existing.Container.Items);
        }
    }

    public async Task<ItemDto> SetItemImage(string itemId, IFormFile imageFile)
    {
        string userId = CurrentUserId();

        Item item = await _itemData.Get(userId, itemId);

        using Image image = await Image.LoadAsync(imageFile.OpenReadStream());
        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(128, 128),
            Mode = ResizeMode.Max
        }));

        using MemoryStream ms = new();
        await image.SaveAsJpegAsync(ms);

        item.ImageData = ms.ToArray();
        item.ImageContentType = imageFile.ContentType;

        Item updatedItem = await _itemData.Update(item);

        return ItemDto.FromEntity(updatedItem);
    }
}
