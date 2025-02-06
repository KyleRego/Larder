namespace Larder.Models.Interface;

public interface IQuantity
{
    public double Amount { get; set; }
    public string? UnitId { get; set; }
}
