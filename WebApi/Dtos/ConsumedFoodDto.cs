using Larder.Models;

namespace Larder.Dtos;

public class ConsumedFoodDto
{
    public string? Id { get; set; }

    public DateOnly DateConsumed { get; set; }

    public required string FoodName { get; set; }

    public required double ServingsConsumed { get; set; }

    public required double CaloriesConsumed { get; set; }

    public static ConsumedFoodDto FromEntity(ConsumedFood entity)
    {
        return new()
        {
            Id = entity.Id,
            FoodName = entity.FoodName,
            ServingsConsumed = entity.ServingsConsumed,
            CaloriesConsumed = entity.CaloriesConsumed,
            DateConsumed = entity.DateConsumed
        };
    }
}
