using Larder.Models;
using Larder.Models.Interface;

namespace Larder.Dtos;

public class QuantityDto : IQuantity
{
    public string? Id { get; set; }
    public double Amount { get; set; }
    public string? UnitId { get; set; }
    public string? UnitName { get; set; }

    public static QuantityDto FromEntity(Quantity quantity)
    {
        return new()
        {
            Amount = quantity.Amount,
            UnitId = quantity.UnitId,
            UnitName = quantity.Unit?.Name
        };
    }

    public static QuantityDto One()
    {
        return new()
        {
            Amount = 1, UnitId = null, UnitName = null
        };
    }
}
