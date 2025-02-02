using Larder.Dtos;
using Larder.Models.SortOptions;

namespace Larder.Services.Interface;

public interface IIngredientService
{
    public Task<List<IngredientDto>>
        GetIngredients(IngredientSortOptions sortBy, string? searchName);

    public Task<IngredientDto?> GetIngredient(string id);
}
