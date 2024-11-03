using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

public abstract class UserOwnedEntity(string userId) : EntityBase
{
    [ForeignKey(nameof(UserId))]
    public ApplicationUser? User { get; set; }
    public string UserId { get; set; } = userId;
}
