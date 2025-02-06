using Larder.Dtos;
using Larder.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Larder.Models;

[Owned]
public class Quantity : IQuantity
{
    public static Quantity FromDto(QuantityDto dto)
    {
        return new()
        {
            Amount = dto.Amount,
            UnitId = string.IsNullOrWhiteSpace(dto.UnitId) ? null : dto.UnitId
        };
    }

    public static Quantity One()
    {
        return new()
        {
            Amount = 1,
            UnitId = null
        };
    }

    public double Amount { get; set; }

    public string? UnitId { get; set; }

    public Unit? Unit { get; set; }
}
