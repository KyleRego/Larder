using Larder.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Larder.Models;

[Owned]
public class Quantity
{
    public static Quantity? FromDto(QuantityDto? dto)
    {
        if (dto == null) return null;

        return new()
        {
            Amount = dto.Amount,
            UnitId =  string.IsNullOrWhiteSpace(dto.UnitId) ? null : dto.UnitId
        };
    }

    public double Amount { get; set; }

    public string? UnitId { get; set; }

    public Unit? Unit { get; set; }
}
