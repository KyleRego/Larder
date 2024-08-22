using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IFoodEatingService
{
    public Task EatFood(FoodEatingLog dto);
}

public class FoodEatingService(IFoodRepository foodRepository) : IFoodEatingService
{
    private readonly IFoodRepository _foodRepo = foodRepository;

    public async Task EatFood(FoodEatingLog dto)
    {
        ArgumentNullException.ThrowIfNull(nameof(dto.FoodId));

        Food food = await _foodRepo.Get(dto.FoodId)
            ?? throw new ApplicationException("no food was found");

        int servingsConsumed = dto.ServingsConsumed;

        
    }
}
