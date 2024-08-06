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

    public Task<IngredientDto?> UpdateQuantity(QuantityDto ingredient);

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

    public async Task<IngredientDto> CreateIngredient(IngredientDto dto)
    {
        Ingredient ingredient = new()
        {
            Name = dto.Name
        };

        if (dto.Quantity != null)
        {
            ingredient.Quantity = new()
            {
                Amount = dto.Quantity.Amount,
                UnitId = dto.Quantity.UnitId
            };
        }

        await _ingredientRepository.Insert(ingredient);

        return dto;
    }

    public async Task<IngredientDto> UpdateIngredient(IngredientDto dto)
    {
        if (dto.Id == null) throw new ApplicationException("ingredient Id was missing");

        Ingredient ingredient = await _ingredientRepository.Get(dto.Id)
                                        ?? throw new ApplicationException("ingredient not found");

        ingredient.Name = dto.Name;
        
        if (dto.Quantity != null)
        {
            ingredient.Quantity = new()
            {
                Amount = dto.Quantity.Amount,
                UnitId = dto.Quantity.UnitId
            };
        }

        await _ingredientRepository.Update(ingredient);

        return dto;
    }

    public async Task<IngredientDto?> UpdateQuantity(QuantityDto dto)
    {
        if (dto.Id == null) throw new ApplicationException("ingredient Id was missing");

        Ingredient ingredient = await _ingredientRepository.Get(dto.Id)
                                    ?? throw new ApplicationException("ingredient not found");

        if (ingredient.Quantity != null)
        {
            ingredient.Quantity.Amount = dto.Amount;
        }
        else
        {
            ingredient.Quantity = new()
            {
                Amount = dto.Amount
            };
        }

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