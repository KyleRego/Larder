using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IIngredientService
{
    public Task<List<IngredientDto>> GetIngredients(IngredientSortOptions sortBy, string? searchName);

    public Task<IngredientDto?> GetIngredient(string id);

    public Task<IngredientDto> CreateIngredient(IngredientDto ingredientDto);

    public Task<IngredientDto> UpdateIngredient(IngredientDto ingredientDto);

    public Task<IngredientDto?> UpdateQuantity(IngredientQuantityDto ingredient);

    public Task DeleteIngredient(string id);
}

public class IngredientService(IIngredientRepository ingredientRepository) : IIngredientService
{
    private readonly IIngredientRepository _ingredientRepository = ingredientRepository;

    public async Task<IngredientDto?> GetIngredient(string id)
    {
        Ingredient? ingredient = await _ingredientRepository.Get(id);
        if (ingredient == null) return null;

        return IngredientDtoAssembler.Assemble(ingredient);
    }

    public async Task<List<IngredientDto>> GetIngredients(IngredientSortOptions sortBy, string? searchName)
    {
        List<Ingredient> ingredients = await _ingredientRepository.GetAll(sortBy, searchName);

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

        await _ingredientRepository.Insert(ingredient);

        return ingredientDto;
    }

    public async Task<IngredientDto> UpdateIngredient(IngredientDto ingredientDto)
    {
        if (ingredientDto.Id == null)
        {
            throw new ApplicationException("Id was missing");
        }

        Ingredient? ingredient = await _ingredientRepository.Get(ingredientDto.Id);
        if (ingredient == null)
        {
            throw new ApplicationException("Ingredient was not found");
        }

        ingredient.Name = ingredientDto.Name;
        ingredient.Quantity = ingredientDto.Quantity;

        if (!string.IsNullOrWhiteSpace(ingredientDto.UnitId))
        {
            ingredient.UnitId = ingredientDto.UnitId;
        }

        await _ingredientRepository.Update(ingredient);

        return ingredientDto;
    }

    public async Task<IngredientDto?> UpdateQuantity(IngredientQuantityDto ingredientDto)
    {
        Ingredient? ingredient = await _ingredientRepository.Get(ingredientDto.Id);

        if (ingredient == null) return null;

        ingredient.Quantity = ingredientDto.Quantity;

        ingredient = await _ingredientRepository.Update(ingredient);

        return IngredientDtoAssembler.Assemble(ingredient);
    }

    public async Task DeleteIngredient(string id)
    {
        Ingredient? ingredient = await _ingredientRepository.Get(id);
        ArgumentNullException.ThrowIfNull(ingredient);

        await _ingredientRepository.Delete(ingredient);
    }
}