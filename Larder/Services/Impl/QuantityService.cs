using Larder.Dtos;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class QuantityService(IServiceProviderWrapper serviceProvider,
                                    IUnitService unitService,
                                    IUnitConversionService unitConvService)
                        : AppServiceBase(serviceProvider), IQuantityService
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
    public async Task<QuantityDto> Subtract(QuantityDto minuend, QuantityDto subtrahend)
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
            QuantityDto convertedSubtrahend = await Convert(subtrahend, minuend.UnitId);

            return new()
            {
                UnitId = minuend.UnitId,
                Amount = minuend.Amount - convertedSubtrahend.Amount
            };
        }
        else
        {
            throw new ApplicationException("");
        }
    }

    /// <summary>
    /// Converts quantity to a specified unit
    /// </summary>
    /// <param name="quantity"></param>
    /// <param name="desiredUnitId">ID of unit to convert quantity to</param>
    /// <returns></returns>
    public async Task<QuantityDto> Convert(QuantityDto quantity,
                                            string desiredUnitId)
    {
        if (quantity.UnitId == null)
            throw new ApplicationException("Quantity must have a unit to be converted");

        if (quantity.UnitId == desiredUnitId)
            return quantity;

        UnitConversionDto conversion = await _unitConvService
                                    .FindConversion(quantity.UnitId, desiredUnitId) ??
            throw new ApplicationException(
$"There is a no unit conversion between units with IDs {quantity.UnitId} and {desiredUnitId}");

        if (quantity.UnitId == conversion.UnitId && desiredUnitId == conversion.TargetUnitId)
        {
            QuantityDto result = new()
            {
                UnitId = desiredUnitId,
                Amount = quantity.Amount * conversion.TargetUnitsPerUnit
            };

            return result;
        }
        else if (quantity.UnitId == conversion.TargetUnitId && desiredUnitId == conversion.UnitId)
        {
            double inverseTargetUnitsPerUnit = 1 / conversion.TargetUnitsPerUnit;

            QuantityDto result = new()
            {
                UnitId = desiredUnitId,
                Amount = quantity.Amount * inverseTargetUnitsPerUnit
            };

            return result;
        }
        else
        {
            throw new ApplicationException(
                "Conversion is not valid to convert this quantity with");
        }
    }

    public async Task<QuantityDto> SubtractUpToZero(QuantityDto minuend,
                                                        QuantityDto subtrahend)
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

    public async Task<double> Divide(QuantityDto dividend, QuantityDto divisor)
    {
        if (dividend.UnitId == null && divisor.UnitId == null)
            return dividend.Amount / divisor.Amount;

        QuantityDto convertedDivisor = await Convert(divisor, dividend.UnitId!);

        return dividend.Amount / convertedDivisor.Amount;
    }
}
