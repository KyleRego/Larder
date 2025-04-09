using Larder.Dtos;
using Larder.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

public class ContainersController(IContainerService containerService)
                                : AppControllerBase
{
    private readonly IContainerService _containerService = containerService;

    [HttpPost("{containerId}/Add/{itemId}")]
    public async Task<ActionResult<ApiResponse<ItemDto?>>>
            PutItemInContainer(string containerId, string itemId)
    {
        try
        {
            ItemDto result = await _containerService.PutItemInContainer(containerId, itemId);

            return new ApiResponse<ItemDto?>(result,
                "Item added", ApiResponseType.Success);
        }
        catch(ApplicationException e)
        {
            return UnprocessableEntity(FromError<ItemDto>(e));
        }
    }

    [HttpPost("{containerId}/Remove/{itemId}")]
    public async Task<ActionResult<ApiResponse<ItemDto?>>>
            RemoveItemFromContainer(string containerId, string itemId)
    {
        try
        {
            ItemDto result = await _containerService.RemoveItemFromContainer(containerId, itemId);

            return new ApiResponse<ItemDto?>(result,
                "Item removed", ApiResponseType.Success);
        }
        catch(ApplicationException e)
        {
            return UnprocessableEntity(FromError<ItemDto>(e));
        }
    }
}
