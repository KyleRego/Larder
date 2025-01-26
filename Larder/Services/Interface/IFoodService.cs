using Larder.Dtos;
using Larder.Repository;

namespace Larder.Services.Interface;

public interface IFoodService
{
    public Task<ItemDto?> GetFood(string id);

    public Task<List<ItemDto>> GetFoods(FoodSortOptions sortOrder,
                                                    string? search);

    public Task<FoodDto> UpdateServings(FoodServingsDto dto);

    public Task<FoodDto> EatFood(FoodServingsDto dto);
}
