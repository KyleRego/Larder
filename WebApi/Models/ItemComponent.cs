using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

public abstract class ItemComponent : EntityBase
{
    public string? ItemId { get; set; }

    [ForeignKey(nameof(ItemId))]
    public required Item Item { get; set; }
}