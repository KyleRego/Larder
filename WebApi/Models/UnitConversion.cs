namespace Larder.Models;

public class UnitConversion(string userId, string unitId, string targetUnitId,
                                    double targetUnitsPerUnit, UnitType type)
                                                    : UserOwnedEntity(userId)
{
    public string UnitId { get; set; } = unitId;
    public Unit? Unit { get; set; }

    public string TargetUnitId { get; set; } = targetUnitId;
    public Unit? TargetUnit { get; set; }

    public double TargetUnitsPerUnit { get; set; } = targetUnitsPerUnit;

    public UnitType UnitType { get; set; } = type;
}
