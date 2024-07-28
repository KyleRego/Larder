namespace Larder.Dtos;

public abstract class ItemDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }
}