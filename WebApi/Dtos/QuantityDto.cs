using Larder.Models;

namespace Larder.Dtos;

public class QuantityDto : DtoBase
{
    public double Amount { get; set; }

    public string? UnitId { get; set; }

    public string? UnitName { get; set; }
}

public static class QuantityDtoAssembler
{
    public static QuantityDto? Assemble(Quantity? quantity)
    {
        if (quantity == null) return null;

        return new()
        {
            Amount = quantity.Amount,
            UnitId = quantity.UnitId,
            UnitName = quantity.Unit?.Name
        };
    }
}