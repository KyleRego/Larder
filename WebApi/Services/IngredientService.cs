using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IIngredientService
{
    public Task<List<IngredientDto>>
        GetIngredients(IngredientSortOptions sortBy, string? searchName);

    public Task<IngredientDto?> GetIngredient(string id);

    public Task<IngredientDto> CreateIngredient(IngredientDto ingredientDto);

    public Task<IngredientDto> UpdateIngredient(IngredientDto ingredientDto);

    public Task<IngredientDto?> UpdateQuantity(QuantityDto ingredient);

    public Task DeleteIngredient(string id);
}

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
            ArgumentNullException.ThrowIfNull(ingItem.Ingredient);
            ingredientDtos.Add(IngredientDto.FromEntity(ingItem));
        }

        return ingredientDtos;
    }

    public async Task<IngredientDto> CreateIngredient(IngredientDto dto)
    {
        Item ingItem = new(CurrentUserId(), dto.Name, dto.Description);

        Ingredient ingredient = new()
        {
            Item = ingItem,
            Quantity = Quantity.FromDto(dto.Quantity),
        };
        ingItem.Ingredient = ingredient;

        await _repository.Insert(ingItem);

        return dto;
    }

    public async Task<IngredientDto> UpdateIngredient(IngredientDto dto)
    {
        if (dto.Id == null) 
            throw new ApplicationException("ingredient Id was missing");

        Item ingItem = await _repository.Get(CurrentUserId(), dto.Id)
                ?? throw new ApplicationException("ingredient not found");
        ArgumentNullException.ThrowIfNull(ingItem.Ingredient);

        ingItem.Name = dto.Name;
        ingItem.Ingredient.Quantity = Quantity.FromDto(dto.Quantity);

        await _repository.Update(ingItem);

        return dto;
    }

    public async Task<IngredientDto?> UpdateQuantity(QuantityDto dto)
    {
        if (dto.Id == null) 
                throw new ApplicationException("ingredient Id was missing");

        Item ingItem = await _repository.Get(CurrentUserId(), dto.Id)
                    ?? throw new ApplicationException("ingredient not found");
        ArgumentNullException.ThrowIfNull(ingItem.Ingredient);

        ingItem.Ingredient.Quantity = Quantity.FromDto(dto);

        ingItem = await _repository.Update(ingItem);

        return IngredientDto.FromEntity(ingItem);
    }

    public async Task DeleteIngredient(string id)
    {
        Item ingItem = await _repository.Get(CurrentUserId(), id)
                    ?? throw new ApplicationException("ingredient not found");

        await _repository.Delete(ingItem);
    }
}
