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
}
