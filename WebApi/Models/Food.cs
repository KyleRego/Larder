namespace Larder.Models;

/// <summary>
/// A food is a serving of food ready to be eaten
/// </summary>
public class Food : Item
{
    public Recipe? Recipe { get; set; }

    public double Calories { get; set; }
}
