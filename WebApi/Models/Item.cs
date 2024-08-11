using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

public abstract class Item : EntityBase
{
    [Required(AllowEmptyStrings = false)]
    public required string Name { get; set; }

    public string? Description { get; set; }

    // TODO: Allow uploading an image for the item

    public Quantity? Quantity { get; set; }
}