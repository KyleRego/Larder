using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

public class RecipeStep(string userId) : UserOwnedEntity(userId)
{
    [Required]
    public required string RecipeId { get; set; }

    [ForeignKey(nameof(RecipeId))]
    public Recipe? Recipe { get; set; }

    [Required]
    public required string Description { get; set; }
}
