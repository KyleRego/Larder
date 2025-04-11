using Microsoft.AspNetCore.Mvc;

using Larder.Dtos;
using Larder.Services.Interface;
using Larder.Models.SortOptions;
using Larder.Models;

namespace Larder.Controllers;

public class ItemsController(IItemService itemService)
            : CrudControllerBase<ItemDto, Item>(itemService)
{
    private readonly IItemService _itemService = itemService;

    [HttpGet]
    public async Task<ActionResult<List<ItemDto>>> Index(
        string? sortOrder, string? search)
    {
        try
        {
            if (sortOrder != null && Enum.TryParse(sortOrder,
                                                out ItemSortOptions sortBy))
            {
                return await _itemService.GetItems(sortBy, search);
            }
            else
            {
                return await _itemService.GetItems(ItemSortOptions.AnyOrder,
                                                                    search);
            }
        }
        catch(ApplicationException)
        {
            return UnprocessableEntity(
                new {error = "Something went wrong retrieving the items"});
        }
    }

    [HttpPost("{id}/image")]
    public async Task<ActionResult<ApiResponse<ItemDto?>>> UploadItemImage(string id, [FromForm] IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0) return BadRequest();

        ItemDto result = await _itemService.SetItemImage(id, imageFile);

        return new ApiResponse<ItemDto?>(result, "Item image updated", ApiResponseType.Success);
    }

    [HttpGet("{id}/image")]
    public async Task<IActionResult> GetItemImage(string id)
    {
        ItemImageDto? image = await _itemService.GetItemImage(id);
        if (image == null)
            return NotFound();

        return File(image.ImageData, image.ImageContentType);
    }
}
