using Larder.Models;

namespace Larder.Dtos;

public class UnitDto
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required UnitType Type { get; set; }

    public static UnitDto FromEntity(Unit entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Type = entity.Type
        };
    }
}
