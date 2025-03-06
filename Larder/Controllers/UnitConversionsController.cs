using Larder.Dtos;
using Larder.Models;
using Larder.Services.Interface;

namespace Larder.Controllers;

public class UnitConversionsController(IUnitConversionService service)
    : CrudControllerBase<UnitConversionDto, UnitConversion>(service)
{
    private readonly IUnitConversionService _service = service;
}
