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

}
