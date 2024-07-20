using System.ComponentModel.DataAnnotations;

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
}