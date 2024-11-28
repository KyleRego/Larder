namespace Larder.Dtos;

public class NutritionDayDto
{
    public required DateOnly Date { get; set; }

    public required double TotalCalories { get; set; }

    public required double TotalProtein { get; set; }

    public List<ConsumedFoodDto> ConsumedFoods { get; set; } = [];
}
