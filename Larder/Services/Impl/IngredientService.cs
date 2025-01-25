using Larder.Dtos;
using Larder.Models;
using Larder.Models.ItemComponent;
using Larder.Repository;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class IngredientService(IServiceProviderWrapper serviceProvider,
                                            IIngredientRepository repository)
                : AppServiceBase(serviceProvider), IIngredientService
{
    private readonly IIngredientRepository _repository = repository;

    public async Task<IngredientDto?> GetIngredient(string id)
    {
        Item? ingItem = await _repository.Get(CurrentUserId(), id);

        if (ingItem == null) return null;

        return IngredientDto.FromEntity(ingItem);
    }

    public async Task<List<IngredientDto>> GetIngredients(
                            IngredientSortOptions sortBy, string? searchName)
    {
        List<Item> ingItems = await _repository.GetAll(
                                        CurrentUserId(), sortBy, searchName);

        List<IngredientDto> ingredientDtos = [];

        foreach (Item ingItem in ingItems)
        {
            Ingredient? ingredient = ingItem.Ingredient;
            ArgumentNullException.ThrowIfNull(ingredient);
            ingredientDtos.Add(IngredientDto.FromEntity(ingItem));
        }

        return ingredientDtos;
    }
}
