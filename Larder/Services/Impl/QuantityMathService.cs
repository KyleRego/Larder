using Larder.Dtos;
using Larder.Models;
using Larder.Models.Interface;
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
    public async Task<QuantityDto> Subtract(IQuantity minuend, IQuantity subtrahend)
    {
        if (minuend.UnitId == null && subtrahend.UnitId == null)
        {
            return new()
            {
                UnitId = null,
                Amount = minuend.Amount - subtrahend.Amount
            };
        }
        else if (minuend.UnitId != null && minuend.UnitId == subtrahend.UnitId)
        {
            return new()
            {
                UnitId = minuend.UnitId,
                Amount = minuend.Amount - subtrahend.Amount
            };
        }
        else if (minuend.UnitId != null && subtrahend.UnitId != null)
        {
            UnitDto minuendUnit = await _unitService.GetUnit(minuend.UnitId)
                ?? throw new ApplicationException("Unit not found for unit ID 1");
            UnitDto subtrahendUnit = await _unitService.GetUnit(subtrahend.UnitId)
                ?? throw new ApplicationException("Unit not found for unit ID 2");

            if (minuendUnit.Type != subtrahendUnit.Type)
            {
                throw new ApplicationException
                    ("Cannot subtract quantities of different unit type");
            }

            UnitConversionDto conversion = await _unitConvService
                                        .FindConversion(minuend.UnitId, subtrahend.UnitId) ??
                throw new ApplicationException(
                    "There is no unit conversion configured for these units");

            QuantityDto convertedSubtrahend = ConvertQuantity(subtrahend, conversion, minuendUnit);

            return new()
            {
                UnitId = minuend.UnitId,
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
    public static QuantityDto ConvertQuantity(IQuantity quantity,
                                            UnitConversionDto conversion,
                                            UnitDto desiredUnit)
    {
        if (quantity.UnitId == null)
            throw new ApplicationException("Quantity must have a unit to be converted");

        if (quantity.UnitId == desiredUnit.Id)
            return (QuantityDto)quantity;

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
            throw new ApplicationException(
                "Conversion is not valid to convert this quantity with");
        }
    }

    public async Task<QuantityDto> SubtractUpToZero(IQuantity minuend, IQuantity subtrahend)
    {
        QuantityDto fullSubtractResult = await Subtract(minuend, subtrahend);

        if (fullSubtractResult.UnitId != minuend.UnitId)
            throw new InvalidOperationException();

        if (fullSubtractResult.Amount <= 0)
        {
            return new() { UnitId = minuend.UnitId, Amount = 0 };
        }
        else
        {
            return fullSubtractResult;
        }
    }
}
