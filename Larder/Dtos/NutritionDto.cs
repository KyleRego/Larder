using Larder.Models;
using Larder.Models.ItemComponents;

namespace Larder.Dtos;

public class NutritionDto
{
    public required QuantityDto ServingSize { get; set; }

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

    public static NutritionDto FromEntity(Nutrition food)
    {
        return new()
        {
            ServingSize = QuantityDto.FromEntity(food.ServingSize),

            Calories = food.Calories,
            GramsProtein = food.GramsProtein,

            GramsTotalFat = food.GramsTotalFat,
            GramsSaturatedFat = food.GramsSaturatedFat,
            GramsTransFat = food.GramsTransFat,

            MilligramsCholesterol = food.MilligramsCholesterol,
            MilligramsSodium = food.MilligramsSodium,

            GramsTotalCarbs = food.GramsTotalCarbs,
            GramsDietaryFiber = food.GramsDietaryFiber,
            GramsTotalSugars = food.GramsTotalSugars,

        };
    }
}
