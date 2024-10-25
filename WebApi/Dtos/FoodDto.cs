using Larder.Models;

namespace Larder.Dtos;

public class FoodDto
{
    public string? Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public double Servings { get; set; }

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

    public double TotalCalories { get; set; }
    public double TotalGramsProtein { get; set; }

    public static FoodDto FromEntity(Item foodItem)
    {
        ArgumentNullException.ThrowIfNull(foodItem.Food);

        return new()
        {
            Id = foodItem.Id,
            Name = foodItem.Name,
            Description = foodItem.Description,
            Servings = foodItem.Food.Servings,

            Calories = foodItem.Food.Calories,
            GramsProtein = foodItem.Food.GramsProtein,

            GramsTotalFat = foodItem.Food.GramsTotalFat,
            GramsSaturatedFat = foodItem.Food.GramsSaturatedFat,
            GramsTransFat = foodItem.Food.GramsTransFat,

            MilligramsCholesterol = foodItem.Food.MilligramsCholesterol,
            MilligramsSodium = foodItem.Food.MilligramsSodium,

            GramsTotalCarbs = foodItem.Food.GramsTotalCarbs,
            GramsDietaryFiber = foodItem.Food.GramsDietaryFiber,
            GramsTotalSugars = foodItem.Food.GramsTotalSugars,

            TotalCalories = foodItem.Food.TotalCalories,
            TotalGramsProtein = foodItem.Food.TotalGramsProtein
        };
    }
}
