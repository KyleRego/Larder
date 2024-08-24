using Larder.Models;

namespace Larder.Dtos;

public class FoodDto
{
    public string? Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public double Servings { get; set; }

    public string? RecipeId { get; set; }

    public double Calories { get; set; }

    public QuantityDto? Protein { get; set; }

    public QuantityDto? TotalFat { get; set; }

    public QuantityDto? SaturatedFat { get; set; }

    public QuantityDto? TransFat { get; set; }

    public QuantityDto? Cholesterol { get; set; }

    public QuantityDto? Sodium { get; set; }

    public QuantityDto? TotalCarbs { get; set; }

    public QuantityDto? DietaryFiber { get; set; }

    public QuantityDto? TotalSugars { get; set; }

    public static FoodDto FromEntity(Food food)
    {
        return new()
        {
            Id = food.Id,
            Name = food.Name,
            Description = food.Description,
            Servings = food.Servings,
            RecipeId = food.Recipe?.Id,
            Calories = food.Calories,
            Protein = QuantityDto.FromEntity(food.Protein),
            TotalFat = QuantityDto.FromEntity(food.TotalFat),
            SaturatedFat = QuantityDto.FromEntity(food.SaturatedFat),
            TransFat = QuantityDto.FromEntity(food.TransFat),
            Cholesterol = QuantityDto.FromEntity(food.Cholesterol),
            Sodium = QuantityDto.FromEntity(food.Sodium),
            TotalCarbs = QuantityDto.FromEntity(food.TotalCarbs),
            DietaryFiber = QuantityDto.FromEntity(food.DietaryFiber),
            TotalSugars = QuantityDto.FromEntity(food.TotalSugars)
        };
    }
}
