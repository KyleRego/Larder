using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

/// <summary>
/// A food is something ready to be eaten
/// </summary>
public class Food : Item
{
    public Recipe? Recipe { get; set; }

    public int Servings { get; set; }

    public int Calories { get; set; }
}