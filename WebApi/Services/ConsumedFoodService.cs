using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IConsumedFoodService
{
    public Task<ConsumedFoodDto> CreateConsumedFood(ConsumedFoodDto dto);
    public Task<ConsumedFoodDto> UpdateConsumedFood(ConsumedFoodDto dto);
    public Task DeleteConsumedFood(string id);
}

public class ConsumedFoodService(IServiceProviderWrapper serviceProvider,
                                            IConsumedFoodRepository repository)
                : AppServiceBase(serviceProvider), IConsumedFoodService
{
    private readonly IConsumedFoodRepository _repository = repository;

    public async Task<ConsumedFoodDto> CreateConsumedFood(ConsumedFoodDto dto)
    {
        ConsumedFood entity = new()
        {
            UserId = CurrentUserId(),
            FoodName = dto.Name,
            DateConsumed = dto.DateConsumed,

            CaloriesConsumed = dto.Calories,
            GramsProteinConsumed = dto.GramsProtein,

            GramsTotalFatConsumed = dto.GramsTotalFat,
            GramsSaturatedFatConsumed = dto.GramsSaturatedFat,
            GramsTransFatConsumed = dto.GramsTransFat,

            MilligramsCholesterolConsumed = dto.MilligramsCholesterol,
            MilligramsSodiumConsumed = dto.MilligramsSodium,

            GramsTotalCarbsConsumed = dto.GramsTotalCarbs,
            GramsDietaryFiberConsumed = dto.GramsDietaryFiber,
            GramsTotalSugarsConsumed = dto.GramsTotalSugars
        };

        await _repository.Insert(entity);

        return ConsumedFoodDto.FromEntity(entity);
    }

    public async Task<ConsumedFoodDto> UpdateConsumedFood(ConsumedFoodDto dto)
    {
        string id = dto.Id
            ?? throw new ApplicationException("id of consumed food missing");

        ConsumedFood entity = await _repository.Get(id)
            ?? throw new ApplicationException("consumed food not found");

        await ThrowIfUserCannotAccess(entity);

        // This does not update the DateConsumed

        entity.FoodName = dto.Name;
        entity.CaloriesConsumed = dto.Calories;
        entity.GramsProteinConsumed = dto.GramsProtein;

        entity.GramsTotalFatConsumed = dto.GramsTotalFat;
        entity.GramsSaturatedFatConsumed = dto.GramsSaturatedFat;
        entity.GramsTransFatConsumed = dto.GramsTransFat;

        entity.MilligramsCholesterolConsumed = dto.MilligramsCholesterol;
        entity.MilligramsSodiumConsumed = dto.MilligramsSodium;

        entity.GramsTotalCarbsConsumed = dto.GramsTotalCarbs;
        entity.GramsDietaryFiberConsumed = dto.GramsDietaryFiber;
        entity.GramsTotalSugarsConsumed = dto.GramsTotalSugars;

        await _repository.Update(entity);

        return ConsumedFoodDto.FromEntity(entity);
    }

    public async Task DeleteConsumedFood(string id)
    {
        ConsumedFood entity = await _repository.Get(id)
            ?? throw new ApplicationException("consumed food not found");

        await ThrowIfUserCannotAccess(entity);

        await _repository.Delete(entity);
    }
}
