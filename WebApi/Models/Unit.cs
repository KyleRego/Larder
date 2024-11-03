using System.ComponentModel.DataAnnotations;

namespace Larder.Models;

public enum UnitType
{
    Mass,
    Volume,
    Weight
}

public class Unit(string userId, string name, UnitType type)
                                    : UserOwnedEntity(userId)
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = name;

    [Required]
    public UnitType Type { get; set; } = type;

    public List<UnitConversion> Conversions { get; set; } = [];

    public List<UnitConversion> TargetConversions { get; set; } = [];
}
