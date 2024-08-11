using Larder.Models;

namespace Larder.Dtos;

public class UnitDto : DtoBase
{
    public required string Name { get; set; }

    public required UnitType Type { get; set; }
}

public static class UnitDtoAssembler
{
    public static UnitDto Assemble(Unit entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Type = entity.Type
        };
    }
}