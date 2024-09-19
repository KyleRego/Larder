namespace Larder.Models;

/// <summary>
/// A food is a serving of food ready to be eaten
/// </summary>
public class Food : Item
{
    public double Servings { get; set; }

    public Quantity ServingSize { get; set; } = new() { Amount = 1, UnitId = null};

    public double Calories { get; set; }

    public double GramsProtein { get; set; }


    public double GramsTotalFat { get; set; }

    public double GramsSaturatedFat { get; set; }

    public double GramsTransFat { get; set; }

    public double MilligramsCholesterol { get; set; }

    public double MilligramsSodium { get; set; }

    public double GramsTotalCarbs { get; set; }

    public double GramsDietaryFiber { get; set; }

    public double GramsTotalSugars { get; set; }

    // TotalCalories should be equal to Servings X Calories etc
    public double TotalCalories { get; set; }
    public double TotalGramsProtein { get; set; }
}
