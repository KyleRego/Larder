using Larder.Dtos;
using Larder.Helpers;
using Larder.Models;
using Larder.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Larder.Services;

public interface IRecipeService
{
    public Task<RecipeDto?> GetRecipe(string id);
    public Task<List<RecipeDto>> GetRecipes(RecipeSortOptions sortBy,
                                                string? searchName);
    public Task<RecipeDto> CreateRecipe(RecipeDto recipeDto);
    public Task<RecipeDto> UpdateRecipe(RecipeDto recipeDto);
    public Task<CookRecipeResultDto> CookRecipe(
                                    CookRecipeDto cookedRecipeDto);
    public Task DeleteRecipe(string id);
}

public class RecipeService( IRecipeRepository repository,
                            IIngredientRepository ingRepo,
                            IFoodRepository foodRepo,
                            IUnitConversionRepository unitConvRepo,
                            IHttpContextAccessor httpConAcsr,
                            IAuthorizationService authService)
                     : ApplicationServiceBase(httpConAcsr, authService), IRecipeService
{
    private readonly IRecipeRepository _repository = repository;
    private readonly IIngredientRepository _ingRepo = ingRepo;
    private readonly IFoodRepository _foodRepo = foodRepo;
    private readonly IUnitConversionRepository _unitConvRepo = unitConvRepo;

    public async Task<CookRecipeResultDto> CookRecipe(CookRecipeDto cookedRecipeDto)
    {
        CookRecipeResultDto result = new();

        Recipe recipe = await _repository.Get(cookedRecipeDto.RecipeId)
                ?? throw new ApplicationException("recipe was not found");

        await ThrowIfUserCannotAccess(recipe);

        foreach(RecipeIngredient recipeIngredient in recipe.RecipeIngredients)
        {
            Ingredient ingredient = recipeIngredient.Ingredient;

            string? ingredientUnitId = ingredient.Quantity.UnitId;
            string? recipeIngredientUnitId = recipeIngredient.Quantity.UnitId;

            if (ingredientUnitId == recipeIngredientUnitId)
            {
                ingredient.Quantity.Amount -= recipeIngredient.Quantity.Amount;
                
            }
            else if (ingredient.Quantity.Unit != null &&
                        recipeIngredient.Quantity.Unit != null)
            {
                UnitConversion? conversion = 
                    await _unitConvRepo.FindByUnitIdsEitherWay(
                        CurrentUserId(), ingredient.Quantity.Unit.Id,
                                            recipeIngredient.Quantity.Unit.Id);
                
                if (conversion != null)
                {
                    Quantity quantityUsed = QuantityConverter.Convert
                        (recipeIngredient.Quantity, conversion,
                                                ingredient.Quantity.Unit);
                
                    ingredient.Quantity.Amount -= quantityUsed.Amount;

                    // that could result in the ingredient quantity being below 0
                }
                else
                {
                    throw new ApplicationException("recipe ingredient and ingredient units do not have a conversion");
                }
            }
            else
            {
                throw new ApplicationException("recipe ingredient quantity and ingredient do not both have units");
            }

            result.Ingredients.Add(IngredientDto.FromEntity(ingredient));
        }

        Food food = await _foodRepo.FindOrCreateBy(CurrentUserId(), recipe.Name);
        food.Servings += recipe.ServingsProduced;

        await _repository.Update(recipe);
        await _foodRepo.Update(food);

        return result;
    }

    public async Task<RecipeDto> CreateRecipe(RecipeDto recipeDto)
    {
        Recipe recipe = new()
        {
            UserId = CurrentUserId(),
            Name = recipeDto.Name
        };

        List<RecipeIngredient> recipeIngredients = [];

        foreach (RecipeIngredientDto ingredientDto in recipeDto.Ingredients)
        {
            if (ingredientDto.Quantity.UnitId == "")
            {
                ingredientDto.Quantity.UnitId = null;
            }

            Ingredient ingredient = await _ingRepo.FindOrCreateBy(
                                CurrentUserId(), ingredientDto.Name);

            RecipeIngredient recipeIngredient = new()
            {
                UserId = CurrentUserId(),
                RecipeId = recipe.Id,
                IngredientId = ingredient.Id,
                Quantity = new()
                {
                    Amount = ingredientDto.Quantity.Amount,
                    UnitId = ingredientDto.Quantity.UnitId
                }
            };

            recipeIngredients.Add(recipeIngredient);
        }

        recipe.RecipeIngredients = recipeIngredients;

        await _repository.Insert(recipe);

        return recipeDto;
    }

    public async Task DeleteRecipe(string id)
    {
        Recipe recipe = await _repository.Get(id)
            ?? throw new ApplicationException("recipe to delete not found");

        await ThrowIfUserCannotAccess(recipe);
    
        await _repository.Delete(recipe);
    }

    public async Task<RecipeDto?> GetRecipe(string id)
    {
        Recipe? recipe = await _repository.Get(id);

        if (recipe == null) return null;

        await ThrowIfUserCannotAccess(recipe);

        return RecipeDto.FromEntity(recipe);
    }

    public async Task<List<RecipeDto>> GetRecipes(RecipeSortOptions sortBy, string? searchName)
    {
        List<Recipe> recipes = await _repository.GetAllForUser(CurrentUserId(), sortBy, searchName);
        List<RecipeDto> recipeDtos = [];

        foreach (Recipe recipe in recipes)
        {
            recipeDtos.Add(RecipeDto.FromEntity(recipe));
        }

        return recipeDtos;
    }

    public async Task<RecipeDto> UpdateRecipe(RecipeDto recipeDto)
    {
        if (recipeDto.Id == null) throw new ApplicationException("recipe Id was missing");
    
        Recipe recipe = await _repository.Get(recipeDto.Id)
                                ?? throw new ApplicationException("recipe not found");

        await ThrowIfUserCannotAccess(recipe);

        recipe.Name = recipeDto.Name;

        List<RecipeIngredient> newRecipeIngredients = [];

        foreach(RecipeIngredientDto ingredientDto in recipeDto.Ingredients)
        {
            Ingredient ingredient = recipe.Ingredients
                                .FirstOrDefault(ingr => ingr.Name == ingredientDto.Name)
                ?? await _ingRepo.FindOrCreateBy(CurrentUserId(), ingredientDto.Name);

            RecipeIngredient newRecipeIngredient = new()
            {
                UserId = CurrentUserId(),
                RecipeId = recipe.Id,
                IngredientId = ingredient.Id,
                Quantity = Quantity.FromDto(ingredientDto.Quantity)
            };
            newRecipeIngredients.Add(newRecipeIngredient);
        }

        recipe.RecipeIngredients = newRecipeIngredients;

        return RecipeDto.FromEntity(await _repository.Update(recipe));
    }
}
