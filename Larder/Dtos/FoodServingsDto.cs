namespace Larder.Dtos;

public class FoodServingsDto
{
    public required string FoodId { get; set; }

    public required QuantityDto QuantityEaten { get; set; }
}
