using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Microsoft.AspNetCore.Authorization;

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

public class IngredientService(IIngredientRepository repository,
                                IHttpContextAccessor httpConAcsr,
                                IAuthorizationService authService)
        : ApplicationServiceBase(httpConAcsr, authService), IIngredientService
{
    private readonly IIngredientRepository _repository = repository;

    public async Task<IngredientDto?> GetIngredient(string id)
    {
        Ingredient? entity = await _repository.Get(id);

        if (entity == null) return null;

        await ThrowIfUserCannotAccess(entity);

        return IngredientDto.FromEntity(entity);
    }

    public async Task<List<IngredientDto>> GetIngredients(
                            IngredientSortOptions sortBy, string? searchName)
    {
        List<Ingredient> ingredients = await _repository.GetAllForUser(
                                        CurrentUserId(), sortBy, searchName);

        List<IngredientDto> ingredientDtos = [];

        foreach (Ingredient ingredient in ingredients)
        {
            ingredientDtos.Add(IngredientDto.FromEntity(ingredient));
        }

        return ingredientDtos;
    }

    public async Task<IngredientDto> CreateIngredient(IngredientDto dto)
    {
        Ingredient ingredient = new()
        {
            UserId = CurrentUserId(),
            Name = dto.Name,
            Quantity = Quantity.FromDto(dto.Quantity),
        };

        await _repository.Insert(ingredient);

        return dto;
    }

    public async Task<IngredientDto> UpdateIngredient(IngredientDto dto)
    {
        if (dto.Id == null) 
            throw new ApplicationException("ingredient Id was missing");

        Ingredient ingredient = await _repository.Get(dto.Id)
                ?? throw new ApplicationException("ingredient not found");

        await ThrowIfUserCannotAccess(ingredient);

        ingredient.Name = dto.Name;
        ingredient.Quantity = Quantity.FromDto(dto.Quantity);

        await _repository.Update(ingredient);

        return dto;
    }

    public async Task<IngredientDto?> UpdateQuantity(QuantityDto dto)
    {
        if (dto.Id == null) 
                throw new ApplicationException("ingredient Id was missing");

        Ingredient ingredient = await _repository.Get(dto.Id)
                    ?? throw new ApplicationException("ingredient not found");

        await ThrowIfUserCannotAccess(ingredient);

        ingredient.Quantity = Quantity.FromDto(dto);

        ingredient = await _repository.Update(ingredient);

        return IngredientDto.FromEntity(ingredient);
    }

    public async Task DeleteIngredient(string id)
    {
        Ingredient ingredient = await _repository.Get(id)
                    ?? throw new ApplicationException("ingredient not found");

        await ThrowIfUserCannotAccess(ingredient);

        await _repository.Delete(ingredient);
    }
}
