using Microsoft.AspNetCore.Mvc;

using Larder.Dtos;
using Larder.Services.Interface;
using Larder.Models.SortOptions;

namespace Larder.Controllers;

using ItemActionResult = Task<ActionResult<ApiResponse<ItemDto>>>;

public class ItemsController(IItemService itemService) : AppControllerBase
{
    private readonly IItemService _itemService = itemService;

    [HttpPost]
    public async ItemActionResult Create(ItemDto itemDto)
    {
        try
        {
            ItemDto item = await _itemService.Add(itemDto);
            return new ApiResponse<ItemDto>(item, "Item created", ApiResponseType.Success);
        }
        catch (ApplicationException e)
        {
            return UnprocessableEntity(new {error = e.Message});
        }
    }

    [HttpGet("{id}")]
    public async ItemActionResult Show(string id)
    {
        try
        {
            ItemDto? itemDto = await _itemService.Get(id);

            if (itemDto == null) return NotFound();

            return new ApiResponse<ItemDto>(itemDto, "", ApiResponseType.Success);
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<ItemDto>>> Index(string? sortOrder,
                                                                string? search)
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

    [HttpPut("{id}")]
    public async ItemActionResult Update(ItemDto itemDto, string id)
    {
        if (itemDto.Id != id) return BadRequest();

        try
        {
            ItemDto result = await _itemService.Update(itemDto);
            return new ApiResponse<ItemDto>(result,
                $"Item \"{result.Name}\" was successfully updated",
                ApiResponseType.Success);
        }
        catch (ApplicationException e)
        {
            return UnprocessableEntity(new {error = e.Message});
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await _itemService.Delete(id);
            return Ok();
        }
        catch(ApplicationException e)
        {
            return UnprocessableEntity(new { error = e.Message }); 
        }
    }
}
