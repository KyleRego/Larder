using Larder.Models;

namespace Larder.Dtos;

public class FoodDto : ItemDto
{
    public double Amount { get; set; }

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
}

public static class FoodDtoAssembler
{
    public static FoodDto Assemble(Food food)
    {
        return new()
        {
            Id = food.Id,
            Name = food.Name,
            Description = food.Description,
            Amount = food.Servings,
            RecipeId = food.Recipe?.Id,
            Calories = food.Calories,
            Protein = QuantityDtoAssembler.Assemble(food.Protein),
            TotalFat = QuantityDtoAssembler.Assemble(food.TotalFat),
            SaturatedFat = QuantityDtoAssembler.Assemble(food.SaturatedFat),
            TransFat = QuantityDtoAssembler.Assemble(food.TransFat),
            Cholesterol = QuantityDtoAssembler.Assemble(food.Cholesterol),
            Sodium = QuantityDtoAssembler.Assemble(food.Sodium),
            TotalCarbs = QuantityDtoAssembler.Assemble(food.TotalCarbs),
            DietaryFiber = QuantityDtoAssembler.Assemble(food.DietaryFiber),
            TotalSugars = QuantityDtoAssembler.Assemble(food.TotalSugars)
        };
    }
}
