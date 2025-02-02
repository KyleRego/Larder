using Larder.Dtos;
using Larder.Models.SortOptions;

namespace Larder.Services.Interface;

public interface IFoodService
{
    public Task<List<ItemDto>> GetFoods(FoodSortOptions sortOrder,
                                                    string? search);


    public Task EatFood(FoodServingsDto dto);
}
