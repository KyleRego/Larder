using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

public enum UnitType
{
    Mass,
    Volume,
    Weight
}

public class Unit : EntityBase
{
    [Required(AllowEmptyStrings = false)]
    public required string Name { get; set; }

    [Required]
    public required UnitType Type { get; set; }

    public List<UnitConversion> Conversions { get; set; } = [];

    public List<UnitConversion> TargetConversions { get; set; } = [];
}
