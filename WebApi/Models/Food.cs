using System.ComponentModel.DataAnnotations.Schema;

namespace Larder.Models;

/// <summary>
/// A food is a serving of food ready to be eaten
/// </summary>
public class Food : Item
{
    public Recipe? Recipe { get; set; }

    public int Quantity { get; set; }

    public int Calories { get; set; }
}