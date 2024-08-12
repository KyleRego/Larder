namespace Larder.Dtos;

public abstract class ItemDto : DtoBase
{
    public required string Name { get; set; }

    public string? Description { get; set; }
}