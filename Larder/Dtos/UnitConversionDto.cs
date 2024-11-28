using Larder.Models;

namespace Larder.Dtos;

public class UnitConversionDto
{
    public string? Id { get; set; }
    public required string UnitId { get; set; }
    public required string TargetUnitId { get; set; }
    public required double TargetUnitsPerUnit { get; set; }

    public static UnitConversionDto FromEntity(UnitConversion entity)
    {
        return new()
        {
            Id = entity.Id,
            UnitId = entity.UnitId,
            TargetUnitId = entity.TargetUnitId,
            TargetUnitsPerUnit = entity.TargetUnitsPerUnit
        };
    }
}
