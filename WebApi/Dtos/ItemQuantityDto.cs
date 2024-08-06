using Larder.Models;

namespace Larder.Dtos;

public class ItemQuantityDto
{
    public double Amount { get; set; }

    public string? UnitId { get; set; }

    public string? UnitName { get; set; }
}

public static class ItemQuantityDtoAssembler
{
    public static ItemQuantityDto Assemble(Quantity entity)
    {
        ItemQuantityDto dto = new()
        {
            Amount = entity.Amount,
            UnitId = entity.UnitId
        };

        if (entity.Unit != null)
        {
            dto.UnitName = entity.Unit.Name;
        }

        return dto;
    }
}