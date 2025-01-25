using Microsoft.AspNetCore.Mvc;
using Larder.Dtos;
using Larder.Repository;
using Larder.Services.Interface;

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
            ItemDto item = await _itemService.CreateItem(itemDto);
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
            ItemDto? itemDto = await _itemService.GetItem(id);

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
    public async Task<ActionResult<ItemDto>> Update(ItemDto itemDto, string id)
    {
        if (itemDto.Id != id) return BadRequest();

        try
        {
            return await _itemService.UpdateItem(itemDto);
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
            await _itemService.DeleteItem(id);
            return Ok();
        }
        catch(ApplicationException e)
        {
            return UnprocessableEntity(new { error = e.Message }); 
        }
    }
}
