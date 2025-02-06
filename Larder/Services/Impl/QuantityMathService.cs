using Larder.Dtos;
using Larder.Models;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class QuantityMathService(IServiceProviderWrapper serviceProvider,
                                    IUnitService unitService,
                                    IUnitConversionService unitConvService)
                        : AppServiceBase(serviceProvider), IQuantityMathService
{
    private readonly IUnitService _unitService = unitService;
    private readonly IUnitConversionService _unitConvService = unitConvService;

    /// <summary>
    /// Subtracts subtrahend from minuend and returns
    /// a quantity with the unit of minuend
    /// </summary>
    /// <param name="minuend"></param>
    /// <param name="subtrahend"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<Quantity> Subtract(Quantity minuend, Quantity subtrahend)
    {
        if (minuend.UnitId == null && subtrahend.UnitId == null)
        {
            return new()
            {
                UnitId = null, Unit = null,
                Amount = minuend.Amount - subtrahend.Amount
            };
        }
        else if (minuend.UnitId != null && minuend.UnitId == subtrahend.UnitId)
        {
            return new()
            {
                UnitId = minuend.UnitId, Unit = minuend.Unit,
                Amount = minuend.Amount - subtrahend.Amount
            };
        }
        else if (minuend.UnitId != null && subtrahend.UnitId != null)
        {
            UnitConversionDto conversion = await _unitConvService
                                        .FindConversion(minuend, subtrahend) ??
                throw new ApplicationException("There is no compatible unit conversion");

            UnitDto minuendUnit = (await _unitService.GetUnit(minuend.UnitId))!;

            Quantity convertedSubtrahend = ConvertQuantity(subtrahend, conversion, minuendUnit);

            return new()
            {
                UnitId = minuend.UnitId,
                Unit = minuend.Unit,
                Amount = minuend.Amount - convertedSubtrahend.Amount
            };
        }
        else
        {
            throw new ApplicationException();
        }
    }

    /// <summary>
    /// Uses conversion to convert quantity to a quantity of unit targetUnit
    /// </summary>
    /// <param name="quantity"></param>
    /// <param name="conversion"></param>
    /// <param name="targetUnit"></param>
    /// <returns></returns>
    public static Quantity ConvertQuantity(Quantity quantity,
                                            UnitConversionDto conversion,
                                            UnitDto desiredUnit)
    {
        if (quantity.Unit == null)
            throw new ApplicationException("Quantity must have a unit to be converted");

        if (quantity.Unit.Id == desiredUnit.Id)
            return quantity;

        if (quantity.UnitId == conversion.UnitId && desiredUnit.Id == conversion.TargetUnitId)
        {
            return new()
            {
                UnitId = desiredUnit.Id,
                Amount = quantity.Amount * conversion.TargetUnitsPerUnit
            };
        }
        else if (quantity.UnitId == conversion.TargetUnitId && desiredUnit.Id == conversion.UnitId)
        {
            double inverseTargetUnitsPerUnit = 1 / conversion.TargetUnitsPerUnit;

            return new()
            {
                UnitId = desiredUnit.Id,
                Amount = quantity.Amount * inverseTargetUnitsPerUnit
            };
        }
        else
        {
            throw new ApplicationException("Conversion is not valid to convert this quantity with");
        }
    }
}
