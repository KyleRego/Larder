namespace Larder.Dtos;

public interface IQuantityDto
{
    public double Amount { get; set; }

    public string? UnitId { get; set; }

    public string? UnitName { get; set; }
}

public class QuantityDto : DtoBase, IQuantityDto
{
    public double Amount { get; set; }

    public string? UnitId { get; set; }

    public string? UnitName { get; set; }
}