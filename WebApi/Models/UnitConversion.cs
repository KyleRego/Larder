using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

public class UnitConversion : EntityBase
{
    public required string UnitId { get; set; }

    public Unit? Unit { get; set; }

    public required string TargetUnitId { get; set; }

    public Unit? TargetUnit { get; set; }

    public double TargetUnitsPerUnit { get; set; }

    public UnitType UnitType { get; set; }
}
