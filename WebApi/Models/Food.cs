namespace Larder.Models;

/// <summary>
/// A food is a serving of food ready to be eaten
/// </summary>
public class Food : Item
{
    public double Servings { get; set; }

    public Quantity ServingSize { get; set; } = new() { Amount = 1, UnitId = null};

    public Recipe? Recipe { get; set; }

    public double Calories { get; set; }

    public Quantity? Protein { get; set; }

    public Quantity? TotalFat { get; set; }

    public Quantity? SaturatedFat { get; set; }

    public Quantity? TransFat { get; set; }

    public Quantity? Cholesterol { get; set; }

    public Quantity? Sodium { get; set; }

    public Quantity? TotalCarbs { get; set; }

    public Quantity? DietaryFiber { get; set; }

    public Quantity? TotalSugars { get; set; }
}
