using Microsoft.AspNetCore.Mvc;
using Larder.Dtos;
using Larder.Services;
using Larder.Repository;

namespace Larder.Controllers;

public class ItemsController(IItemService itemService) : AppControllerBase
{
    private readonly IItemService _itemService = itemService;

    [HttpPost]
    public async Task<ActionResult<ItemDto>> Create(ItemDto itemDto)
    {
        try
        {
            return await _itemService.CreateItem(itemDto);
        }
        catch (ApplicationException e)
        {
            return UnprocessableEntity(new {error = e.Message});
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> Show(string id)
    {
        try
        {
            ItemDto? itemDto = await _itemService.GetItem(id);

            if (itemDto == null) return NotFound();

            return itemDto;
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
                new {error = "Something went wrong fetching the items"});
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
