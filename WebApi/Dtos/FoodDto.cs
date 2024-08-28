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

    public static FoodDto FromEntity(Food food)
    {
        return new()
        {
            Id = food.Id,
            Name = food.Name,
            Description = food.Description,
            Servings = food.Servings,

            Calories = food.Calories,
            GramsProtein = food.GramsProtein,

            GramsTotalFat = food.GramsTotalFat,
            GramsSaturatedFat = food.GramsSaturatedFat,
            GramsTransFat = food.GramsTransFat,

            MilligramsCholesterol = food.MilligramsCholesterol,
            MilligramsSodium = food.MilligramsSodium,

            GramsTotalCarbs = food.GramsTotalCarbs,
            GramsDietaryFiber = food.GramsDietaryFiber,
            GramsTotalSugars = food.GramsTotalSugars
        };
    }
}
