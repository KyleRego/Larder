using Larder.Models;

namespace Larder.Helpers;

public static class QuantityConverter
{
    /// <summary>
    /// Uses conversion to convert quantity to a quantity of unit targetUnit
    /// </summary>
    /// <param name="quantity"></param>
    /// <param name="conversion"></param>
    /// <param name="targetUnit"></param>
    /// <returns></returns>
    public static Quantity Convert(Quantity quantity, UnitConversion conversion, Unit desiredUnit)
    {
        if (quantity.Unit == null)
            throw new ApplicationException("quantity must have a unit to be converted");

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
            throw new ApplicationException("conversion is not valid to convert this quantity with");
        }
    }
}
