using Larder.Dtos;
using Larder.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Larder.Models;

[Owned]
public class Quantity : IQuantity
{
    public double Amount { get; set; }

    public string? UnitId { get; set; }

    public Unit? Unit { get; set; }

    public static Quantity FromDto(QuantityDto dto)
    {
        return new()
        {
            Amount = dto.Amount,
            UnitId = string.IsNullOrWhiteSpace(dto.UnitId) ? null : dto.UnitId
        };
    }

    public static explicit operator QuantityDto(Quantity quantity)
    {
        return new QuantityDto
        {
            Amount = quantity.Amount,
            UnitId = quantity.UnitId,
            UnitName = quantity.Unit?.Name
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

    public static Quantity Scalar(int amount)
    {
        return new()
        {
            Amount = amount,
            UnitId = null
        };
    }
}
