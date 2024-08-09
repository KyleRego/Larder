using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

public abstract class Item : EntityBase, IQuantity
{
    [Required(AllowEmptyStrings = false)]
    public required string Name { get; set; }

    public string? Description { get; set; }

    // TODO: Allow uploading an image for the item

    public double Amount { get; set; }

    // This attribute does not seem to be working
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    public string? UnitId { get; set; }

    [ForeignKey(nameof(UnitId))]
    public Unit? Unit { get; set; }
}