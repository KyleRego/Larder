using Larder.Dtos;

namespace Larder.Models.ItemComponents;

public class Food : ItemComponent
{
    public double Servings { get; set; }

    public Quantity ServingSize { get; set; } = new() { Amount = 1, UnitId = null};

    // Calories per serving
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

    public void UpdateTotals()
    {
        TotalCalories = Calories * Servings;
        TotalGramsProtein = GramsProtein * Servings;
    }

    public static Food FromDto(FoodDto dto, Item item)
    {
        Food food = new()
        {
            Item = item,
            Calories = dto.Calories,
            Servings = dto.Servings,
            ServingSize = Quantity.FromDto(dto.ServingSize),
            GramsProtein = dto.GramsProtein,
            GramsTotalFat = dto.GramsTotalFat,
            GramsSaturatedFat = dto.GramsSaturatedFat,
            GramsTransFat = dto.GramsTransFat,
            GramsTotalCarbs = dto.GramsTotalCarbs,
            GramsTotalSugars = dto.GramsTotalSugars,
            GramsDietaryFiber = dto.GramsDietaryFiber,
            MilligramsCholesterol = dto.MilligramsCholesterol,
            MilligramsSodium = dto.MilligramsSodium
        };
        food.UpdateTotals();
        return food;
    }
}
