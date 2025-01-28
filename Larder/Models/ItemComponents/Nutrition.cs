using Larder.Dtos;

namespace Larder.Models.ItemComponents;


public class Nutrition : ItemComponent
{
    public Quantity ServingSize { get; set; }
            = new() { Amount = 1, UnitId = null};

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

    public static Nutrition FromDto(FoodDto dto, Item item)
    {
        Nutrition food = new()
        {
            Item = item,
            Calories = dto.Calories,
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
        return food;
    }
}
