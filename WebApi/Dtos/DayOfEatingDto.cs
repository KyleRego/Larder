using Larder.Models;

namespace Larder.Dtos;

public class DayOfEatingDto
{
    public required DateOnly Date { get; set; }

    public required double TotalCalories { get; set; }

    public required double TotalProtein { get; set; }

    public List<ConsumedFoodDto> ConsumedFoods { get; set; } = [];
}

public class ConsumedFoodDto
{
    public required string FoodName { get; set; }

    public required double ServingsConsumed { get; set; }

    public required double CaloriesConsumed { get; set; }

    public static ConsumedFoodDto FromEntity(ConsumedFood entity)
    {
        return new()
        {
            FoodName = entity.FoodName,
            ServingsConsumed = entity.ServingsConsumed,
            CaloriesConsumed = entity.CaloriesConsumed
        };
    }
}
