using Larder.Models;

namespace Larder.Dtos;

public class QuantityDto
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

    public static QuantityDto Scalar(int amount)
    {
        return new()
        {
            Amount = amount, UnitId = null, UnitName = null
        };
    }
}
