using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

public class UserOwnedEntity : EntityBase
{
    [ForeignKey(nameof(UserId))]
    public ApplicationUser? User { get; set; }
    public required string UserId { get; set; }
}
