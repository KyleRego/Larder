using System.Text.RegularExpressions;
using Larder.Dtos;
using Larder.Models;
using Larder.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Larder.Controllers;

public abstract class CrudControllerBase<TDto, TEntity>
            (ICrudServiceBase<TDto, TEntity> crudService)
                    : AppControllerBase
    where TDto : EntityDto<TEntity>
    where TEntity : UserOwnedEntity
{
    private readonly ICrudServiceBase<TDto, TEntity> _crudService = crudService;
    private static readonly string entityName =
        Regex.Replace(typeof(TEntity).Name, "(?<!^)([A-Z])", " $1").ToLower();

    [HttpPost]
    public async Task<ActionResult<ApiResponse<TDto>>> Create(TDto dto)
    {
        try
        {
            TDto resultDto = await _crudService.Add(dto);
            return new ApiResponse<TDto>(resultDto,
                $"Successfully created {entityName}",
                ApiResponseType.Success);
        }
        catch (ApplicationException e)
        {
            return UnprocessableEntity(new {error = e.Message});
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<TDto>>> Show(string id)
    {
        try
        {
            TDto? resultDto = await _crudService.Get(id);

            if (resultDto == null) return NotFound();

            return new ApiResponse<TDto>(resultDto, "", ApiResponseType.Success);
        }
        catch (ApplicationException)
        {
            return UnprocessableEntity();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<TDto>>> Update(TDto dto, string id)
    {
        if (dto.Id != id) return BadRequest();

        try
        {
            TDto resultDto = await _crudService.Update(dto);
            return new ApiResponse<TDto>(resultDto,
                $"Updated {entityName} successfully",
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
            await _crudService.Delete(id);
            return Ok();
        }
        catch(ApplicationException e)
        {
            return UnprocessableEntity(new { error = e.Message }); 
        }
    }
}
