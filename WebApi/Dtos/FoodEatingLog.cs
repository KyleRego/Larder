namespace Larder.Dtos;

/// <summary>
/// Data Transfer Object for modeling the user action
/// of logging that they ate some food that they want
/// to track.
/// </summary>
public class FoodEatingLog
{
    public required string FoodId { get; set; }

    public required int ServingsConsumed { get; set; }
}