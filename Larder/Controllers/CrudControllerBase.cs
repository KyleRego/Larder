using System.ComponentModel.DataAnnotations;
using System.Reflection;
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

    [HttpGet("meta")]
    public ActionResult<IEnumerable<FieldMeta>> GetMeta()
    {
        var dtoType = typeof(TDto);
        var props = dtoType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p =>
            {
                var required = p.GetCustomAttribute<RequiredAttribute>() != null;
                var strLen   = p.GetCustomAttribute<StringLengthAttribute>()?.MaximumLength;
                var isEnum   = p.PropertyType.IsEnum;
                return new FieldMeta
                {
                    Name       = p.Name,
                    DataType   = isEnum 
                                   ? $"Enum:{p.PropertyType.Name}"
                                   : p.PropertyType.Name,
                    IsRequired = required
                };
            })
            .ToList();

        return Ok(props);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<TDto?>>> Create(TDto dto)
    {
        try
        {
            TDto resultDto = await _crudService.Add(dto);
            return new ApiResponse<TDto?>(
                resultDto, $"Successfully created {entityName}", ApiResponseType.Success);
        }
        catch (ApplicationException e)
        {
            return FromError<TDto>(e);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<TDto?>>> Show(string id)
    {
        try
        {
            TDto? resultDto = await _crudService.Get(id);

            if (resultDto == null) return NotFound();

            return new ApiResponse<TDto?>(resultDto, "", ApiResponseType.Success);
        }
        catch (ApplicationException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<TDto?>>> Update(TDto dto, string id)
    {
        if (dto.Id != id) return BadRequest();

        try
        {
            TDto resultDto = await _crudService.Update(dto);

            return new ApiResponse<TDto?>(
                resultDto, $"Updated {entityName} successfully", ApiResponseType.Success);
        }
        catch (ApplicationException e)
        {
            return FromError<TDto?>(e);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object?>>> Delete(string id)
    {
        try
        {
            await _crudService.Delete(id);

            return Ok();
        }
        catch(ApplicationException e)
        {
            return FromError<object>(e); 
        }
    }
}
