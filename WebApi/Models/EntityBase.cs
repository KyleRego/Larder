using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

public abstract class EntityBase
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey(nameof(UserId))]
    public ApplicationUser? User { get; set; }
    public required string? UserId { get; set; }
}
