using Larder.Dtos;
using Larder.Helpers;
using Larder.Models;
using Larder.Models.ItemComponent;
using Larder.Repository;

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

public class RecipeService(IServiceProviderWrapper serviceProvider,
                                    IRecipeRepository repository,
                                    IIngredientRepository ingRepo,
                                    IFoodRepository foodRepo,
                                    IUnitConversionRepository unitConvRepo)
                     : AppServiceBase(serviceProvider), IRecipeService
{
    private readonly IRecipeRepository _repository = repository;
    private readonly IIngredientRepository _ingRepo = ingRepo;
    private readonly IFoodRepository _foodRepo = foodRepo;
    private readonly IUnitConversionRepository _unitConvRepo = unitConvRepo;

    public async Task<CookRecipeResultDto> CookRecipe(CookRecipeDto cookedRecipeDto)
    {
        CookRecipeResultDto result = new();

        Recipe recipe = await _repository.Get(CurrentUserId(), cookedRecipeDto.RecipeId)
                ?? throw new ApplicationException("recipe was not found");

        foreach(RecipeIngredient recipeIngredient in recipe.RecipeIngredients)
        {
            // TODO: Refactor this to use Item instead of Ingredient
            // since item now has quantity component
            Ingredient ingredient = recipeIngredient.Ingredient;

            string? ingredientUnitId = ingredient.Item.QuantityComp!.Quantity.UnitId;
            string? recipeIngredientUnitId = recipeIngredient.Quantity.UnitId;

            if (ingredientUnitId == recipeIngredientUnitId)
            {
                ingredient.Item.QuantityComp.Quantity.Amount -= recipeIngredient.Quantity.Amount;
                
            }
            else if (ingredient.Item.QuantityComp.Quantity.Unit != null &&
                        recipeIngredient.Quantity.Unit != null)
            {
                UnitConversion? conversion = 
                    await _unitConvRepo.FindByUnitIdsEitherWay(
                        CurrentUserId(), ingredient.Item.QuantityComp.Quantity.Unit.Id,
                                            recipeIngredient.Quantity.Unit.Id);
                
                if (conversion != null)
                {
                    Quantity quantityUsed = QuantityConverter.Convert
                        (recipeIngredient.Quantity, conversion,
                                                ingredient.Item.QuantityComp.Quantity.Unit);
                
                    ingredient.Item.QuantityComp.Quantity.Amount -= quantityUsed.Amount;

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

            result.Ingredients.Add(IngredientDto.FromEntity(ingredient.Item));
        }

        Item foodItem = await _foodRepo.FindOrCreateBy(CurrentUserId(), recipe.Name);
        ArgumentNullException.ThrowIfNull(foodItem.Food);
        foodItem.Food.Servings += recipe.ServingsProduced;

        await _repository.Update(recipe);
        await _foodRepo.Update(foodItem);

        return result;
    }

    public async Task<RecipeDto> CreateRecipe(RecipeDto recipeDto)
    {
        Recipe recipe = new(CurrentUserId(), recipeDto.Name);

        List<RecipeIngredient> recipeIngredients = [];

        foreach (RecipeIngredientDto ingredientDto in recipeDto.Ingredients)
        {
            if (ingredientDto.Quantity.UnitId == "")
            {
                ingredientDto.Quantity.UnitId = null;
            }

            Item ingItem = await _ingRepo.FindOrCreateBy(
                                CurrentUserId(), ingredientDto.Name);
            ArgumentNullException.ThrowIfNull(ingItem.Ingredient);

            Quantity quantity = new()
            {
                Amount = ingredientDto.Quantity.Amount,
                UnitId = ingredientDto.Quantity.UnitId
            };
        
            RecipeIngredient recipeIngredient = new(CurrentUserId(), recipe.Id, ingItem.Id)
            {
                Quantity = quantity
            };

            recipeIngredients.Add(recipeIngredient);
        }

        recipe.RecipeIngredients = recipeIngredients;

        await _repository.Insert(recipe);

        return recipeDto;
    }

    public async Task DeleteRecipe(string id)
    {
        Recipe recipe = await _repository.Get(CurrentUserId(), id)
            ?? throw new ApplicationException("recipe to delete not found");
    
        await _repository.Delete(recipe);
    }

    public async Task<RecipeDto?> GetRecipe(string id)
    {
        Recipe? recipe = await _repository.Get(CurrentUserId(), id);

        if (recipe == null) return null;

        return RecipeDto.FromEntity(recipe);
    }

    public async Task<List<RecipeDto>> GetRecipes(RecipeSortOptions sortBy, string? searchName)
    {
        List<Recipe> recipes = await _repository.GetAll(CurrentUserId(), sortBy, searchName);
        List<RecipeDto> recipeDtos = [];

        foreach (Recipe recipe in recipes)
        {
            recipeDtos.Add(RecipeDto.FromEntity(recipe));
        }

        return recipeDtos;
    }

    public async Task<RecipeDto> UpdateRecipe(RecipeDto recipeDto)
    {
        if (recipeDto.Id == null)
                    throw new ApplicationException("recipe Id was missing");
    
        Recipe recipe = await _repository.Get(CurrentUserId(), recipeDto.Id)
                    ?? throw new ApplicationException("recipe not found");

        recipe.Name = recipeDto.Name;

        List<RecipeIngredient> newRecipeIngredients = [];

        foreach(RecipeIngredientDto ingDto in recipeDto.Ingredients)
        {
            Item ingItem = recipe.Ingredients.FirstOrDefault(ing =>
                                            ing.Item.Name == ingDto.Name)?.Item
                ?? await _ingRepo.FindOrCreateBy(CurrentUserId(), ingDto.Name);

            RecipeIngredient newRecipeIngredient
                            = new(CurrentUserId(), recipe.Id,ingItem.Id)
            {
                Quantity = Quantity.FromDto(ingDto.Quantity)
            };
            newRecipeIngredients.Add(newRecipeIngredient);
        }

        recipe.RecipeIngredients = newRecipeIngredients;

        return RecipeDto.FromEntity(await _repository.Update(recipe));
    }
}
