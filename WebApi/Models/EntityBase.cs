using System.ComponentModel.DataAnnotations;

namespace Larder.Models;

public abstract class EntityBase
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
}