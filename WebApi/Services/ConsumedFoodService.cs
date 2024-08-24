using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IConsumedFoodService
{
    public Task<List<DayOfEatingDto>> FoodsOfPastWeek();
}

public class ConsumedFoodService(IConsumedFoodRepository consumedFoodRepository) : IConsumedFoodService
{
    private readonly IConsumedFoodRepository _consumedFoodRepo = consumedFoodRepository;

    public async Task<List<DayOfEatingDto>> FoodsOfPastWeek()
    {
        List<ConsumedFood> entities = await _consumedFoodRepo.GetConsumedFoodsPastWeek();

        Dictionary<DateOnly, List<ConsumedFood>> map = [];

        foreach (ConsumedFood entity in entities)
        {
            DateOnly dateEatenAt = entity.DateConsumed;

            if (map.TryGetValue(dateEatenAt, out List<ConsumedFood>? foodsThatDay))
            {
                foodsThatDay.Add(entity);
            }
            else
            {
                map[dateEatenAt] = [entity];
            }
        }

        List<DayOfEatingDto> result = [];

        foreach (DateOnly date in map.Keys.Order())
        {
            DayOfEatingDto dayOfEating = new()
            {
                Date = date,
                TotalCalories = 0,
                TotalProtein = 0,
                ConsumedFoods = []
            };

            List<ConsumedFood> foodsThatDay = map[date];

            foreach (ConsumedFood consFood in foodsThatDay)
            {
                ConsumedFoodDto consumedFoodDto = ConsumedFoodDto.FromEntity(consFood);

                dayOfEating.TotalCalories += consumedFoodDto.CaloriesConsumed;
                dayOfEating.ConsumedFoods.Add(consumedFoodDto);
            }

            result.Add(dayOfEating);
        }

        return result;
    }
}
