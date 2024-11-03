namespace Larder.Models;

public class ConsumedFood(string userId) : UserOwnedEntity(userId)
{
    public required string FoodName { get; set; }
    public required DateOnly DateConsumed { get; set; }

    public required double CaloriesConsumed { get; set; }
    public required double GramsProteinConsumed { get; set; }

    public double GramsTotalFatConsumed { get; set; }
    public double GramsSaturatedFatConsumed { get; set; }
    public double GramsTransFatConsumed { get; set; }

    public double MilligramsCholesterolConsumed { get; set; }
    public double MilligramsSodiumConsumed { get; set; }

    public double GramsTotalCarbsConsumed { get; set; }
    public double GramsDietaryFiberConsumed { get; set; }
    public double GramsTotalSugarsConsumed { get; set; }
}
