namespace Larder.Models;

public interface IQuantity
{
    public double Amount { get; set; }

    public string? UnitId { get; set; }

    public Unit? Unit { get; set; }
}