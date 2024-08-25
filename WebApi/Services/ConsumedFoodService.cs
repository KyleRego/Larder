using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IConsumedFoodService
{
    public Task CreateConsumedFood(ConsumedFoodDto dto);
    public Task UpdateConsumedFood(ConsumedFoodDto dto);
    public Task DeleteConsumedFood(string id);
}

public class ConsumedFoodService(IConsumedFoodRepository consumedFoodRepository) : IConsumedFoodService
{
    private readonly IConsumedFoodRepository _consFoodRepo = consumedFoodRepository;

    public async Task CreateConsumedFood(ConsumedFoodDto dto)
    {
        double servingsConsumed = dto.ServingsConsumed;

        ConsumedFood entity = new()
        {
            FoodName = dto.FoodName,
            DateTimeConsumed = null,
            DateConsumed = (DateOnly)dto.DateConsumed,
            ServingsConsumed = dto.ServingsConsumed,
            CaloriesConsumed = servingsConsumed * dto.CaloriesConsumed,
            ProteinConsumed = 0
        };

        await _consFoodRepo.Insert(entity);
    }

    public async Task UpdateConsumedFood(ConsumedFoodDto dto)
    {
        string id = dto.Id ?? throw new ApplicationException("id of consumed food to update missing");

        ConsumedFood entity = await _consFoodRepo.Get(id)
            ?? throw new ApplicationException("consumed food to delete was not found");

        entity.FoodName = dto.FoodName;
        entity.ServingsConsumed = dto.ServingsConsumed;
        entity.CaloriesConsumed = dto.CaloriesConsumed;
        entity.ProteinConsumed = 0;

        await _consFoodRepo.Update(entity);
    }

    public async Task DeleteConsumedFood(string id)
    {
        ConsumedFood entity = await _consFoodRepo.Get(id)
            ?? throw new ApplicationException("consumed food to delete was not found");
    
        await _consFoodRepo.Delete(entity);
    }
}
