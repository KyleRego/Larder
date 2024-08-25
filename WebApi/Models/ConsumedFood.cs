namespace Larder.Models;

public class ConsumedFood : EntityBase
{
    public required string FoodName { get; set; }

    public required DateTime? DateTimeConsumed { get; set; }

    public required DateOnly DateConsumed { get; set; }

    public required double ServingsConsumed { get; set; }

    public required double CaloriesConsumed { get; set; }

    public required double ProteinConsumed { get; set; }
}
