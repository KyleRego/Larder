using System.ComponentModel.DataAnnotations;

namespace Larder.Models;

public abstract class Item : EntityBase
{
    [Required(AllowEmptyStrings = false)]
    public required string Name { get; set; }

    public string? Description { get; set; }

    // TODO: Allow uploading an image for the item
}