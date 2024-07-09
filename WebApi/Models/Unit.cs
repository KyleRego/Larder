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
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public UnitType Type { get; set; }
}