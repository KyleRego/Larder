using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IIngredientService
{
    public Task<List<IngredientDto>> GetIngredients();

    public Task<IngredientDto?> GetIngredient(string id);

    public Task<IngredientDto> CreateIngredient(IngredientDto ingredientDto);

    public Task<IngredientDto?> UpdateQuantity(IngredientQuantityDto ingredient);
}

public class IngredientService(IIngredientRepository ingredientRepository) : IIngredientService
{
    private readonly IIngredientRepository _ingredientRepository = ingredientRepository;

    public async Task<IngredientDto?> GetIngredient(string id)
    {
        Ingredient? ingredient = await _ingredientRepository.GetIngredient(id, true);
        if (ingredient == null) return null;

        return IngredientDtoAssembler.Assemble(ingredient);
    }

    public async Task<List<IngredientDto>> GetIngredients()
    {
        List<Ingredient> ingredients = await _ingredientRepository.GetIngredients();

        List<IngredientDto> ingredientDtos = [];

        foreach (Ingredient ingredient in ingredients)
        {
            ingredientDtos.Add(IngredientDtoAssembler.Assemble(ingredient));
        }

        return ingredientDtos;
    }

    public async Task<IngredientDto> CreateIngredient(IngredientDto ingredientDto)
    {
        Ingredient ingredient = new()
        {
            Name = ingredientDto.Name,
            Quantity = ingredientDto.Quantity,
            UnitId = ingredientDto.UnitId
        };

        await _ingredientRepository.InsertIngredient(ingredient);

        return ingredientDto;
    }

    public async Task<IngredientDto?> UpdateQuantity(IngredientQuantityDto ingredientDto)
    {
        Ingredient? ingredient = await _ingredientRepository.GetIngredient(ingredientDto.Id, false);

        if (ingredient == null) return null;

        ingredient.Quantity = ingredientDto.Quantity;

        ingredient = await _ingredientRepository.Update(ingredient);

        return IngredientDtoAssembler.Assemble(ingredient);
    }
}