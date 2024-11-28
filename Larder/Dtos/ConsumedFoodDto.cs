using Larder.Models;

namespace Larder.Dtos;

public class ConsumedFoodDto
{
    public string? Id { get; set; }
    public DateOnly DateConsumed { get; set; }
    public required string Name { get; set; }

    public required double Calories { get; set; }
    public required double GramsProtein { get; set; }

    public double GramsTotalFat { get; set; }
    public double GramsSaturatedFat { get; set; }
    public double GramsTransFat { get; set; }

    public double MilligramsCholesterol { get; set; }
    public double MilligramsSodium { get; set; }

    public double GramsTotalCarbs { get; set; }
    public double GramsDietaryFiber { get; set; }
    public double GramsTotalSugars { get; set; }

    public static ConsumedFoodDto FromEntity(ConsumedFood entity)
    {
        return new()
        {
            Id = entity.Id,
            DateConsumed = entity.DateConsumed,
            Name = entity.FoodName,

            Calories = entity.CaloriesConsumed,
            GramsProtein = entity.GramsProteinConsumed,

            GramsTotalFat = entity.GramsTotalFatConsumed,
            GramsSaturatedFat = entity.GramsSaturatedFatConsumed,
            GramsTransFat = entity.GramsTransFatConsumed,

            MilligramsCholesterol = entity.MilligramsCholesterolConsumed,
            MilligramsSodium = entity.MilligramsSodiumConsumed,

            GramsTotalCarbs = entity.GramsTotalCarbsConsumed,
            GramsDietaryFiber = entity.GramsDietaryFiberConsumed,
            GramsTotalSugars = entity.GramsTotalSugarsConsumed,
            
        };
    }
}
