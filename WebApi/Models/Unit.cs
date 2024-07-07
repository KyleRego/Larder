using System.ComponentModel.DataAnnotations;

namespace Larder.Models;

public class Unit : EntityBase
{
    [Required]
    public string Name { get; set; } = "";
}