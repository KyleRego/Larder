using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface ITimelineService
{
    public Task<List<NutritionDayDto>> FoodsOfPastWeek();
}

public class TimelineService(IServiceProviderWrapper serviceProvider,
                                            IConsumedFoodRepository repository)
                    : AppServiceBase(serviceProvider), ITimelineService
{
    private readonly IConsumedFoodRepository _repository = repository;

    public async Task<List<NutritionDayDto>> FoodsOfPastWeek()
    {
        List<ConsumedFood> entities =
            await _repository.GetConsumedFoodsPastWeek(CurrentUserId());

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

        List<NutritionDayDto> result = [];

        foreach (DateOnly date in map.Keys.OrderDescending())
        {
            NutritionDayDto dayOfEating = new()
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

                dayOfEating.TotalCalories += consumedFoodDto.Calories;
                dayOfEating.ConsumedFoods.Add(consumedFoodDto);
            }

            result.Add(dayOfEating);
        }

        return result;
    }
}
