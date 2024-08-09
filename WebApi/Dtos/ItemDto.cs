namespace Larder.Dtos;

public abstract class ItemDto : DtoBase, IQuantityDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public double Amount { get; set; }

    public string? UnitId { get; set; }

    public string? UnitName { get; set; }
}