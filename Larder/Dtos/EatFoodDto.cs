namespace Larder.Dtos;

public class EatFoodDto
{
    public required string ItemId { get; set; }

    public required QuantityDto QuantityEaten { get; set; }
}
