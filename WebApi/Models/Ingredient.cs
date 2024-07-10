using System.ComponentModel.DataAnnotations;

namespace Larder.Models;

public class Ingredient : EntityBase
{
    [Required(AllowEmptyStrings = false)]
    public required string Name { get; set; }
}