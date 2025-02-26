using Larder.Models;

namespace Larder.Dtos;

public class UnitDto : EntityDto<Unit>
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required UnitType Type { get; set; }
    public List<UnitConversionDto> Conversions { get; set; } = [];

    public static UnitDto FromEntity(Unit entity)
    {
        UnitDto result =  new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Type = entity.Type
        };

        foreach (UnitConversion uc in entity.Conversions)
        {
            result.Conversions.Add(UnitConversionDto.FromEntity(uc));
        }

        foreach (UnitConversion uc in entity.TargetConversions)
        {
            result.Conversions.Add(UnitConversionDto.FromEntity(uc));
        }

        return result;
    }
}
