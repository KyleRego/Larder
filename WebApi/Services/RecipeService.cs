using Larder.Dtos;
using Larder.Helpers;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IRecipeService
{
    public Task<RecipeDto?> GetRecipe(string id);
    public Task<List<RecipeDto>> GetRecipes(RecipeSortOptions sortBy, string? searchName);
    public Task<RecipeDto> CreateRecipe(RecipeDto recipeDto);
    public Task<RecipeDto> UpdateRecipe(RecipeDto recipeDto);
    public Task<RecipeDto> CookRecipe(CookRecipeDto cookedRecipeDto);
    public Task DeleteRecipe(string id);
}

public class RecipeService( IRecipeRepository recipeRepository,
                            IIngredientRepository ingredientRepository,
                            IFoodRepository foodRepository,
                            IUnitConversionRepository unitConvRep) : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IIngredientRepository _ingredientRepository = ingredientRepository;
    private readonly IFoodRepository _foodRepository = foodRepository;
    private readonly IUnitConversionRepository _unitConvRep = unitConvRep;

    public async Task<RecipeDto> CookRecipe(CookRecipeDto cookedRecipeDto)
    {
        Recipe recipe = await _recipeRepository.Get(cookedRecipeDto.RecipeId)
                ?? throw new ApplicationException("recipe was not found");

        foreach(RecipeIngredient recipeIngredient in recipe.RecipeIngredients)
        {
            Ingredient ingredient = recipeIngredient.Ingredient;

            string? ingredientUnitId = ingredient.Quantity.UnitId;
            string? recipeIngredientUnitId = recipeIngredient.Quantity.UnitId;

            if (ingredientUnitId == recipeIngredientUnitId)
            {
                ingredient.Quantity.Amount -= recipeIngredient.Quantity.Amount;
            }
            else if (ingredient.Quantity.Unit != null && recipeIngredient.Quantity.Unit != null)
            {
                UnitConversion? conversion = 
                                await _unitConvRep.FindByUnitIdsEitherWay(ingredient.Quantity.Unit.Id,
                                                                    recipeIngredient.Quantity.Unit.Id);
                
                if (conversion != null)
                {
                    Quantity quantityUsed = QuantityConverter.Convert
                        (recipeIngredient.Quantity, conversion, ingredient.Quantity.Unit);
                
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
        }

        Food food = await _foodRepository.FindOrCreateBy(recipe.Name);
        food.Servings += recipe.ServingsProduced;

        await _recipeRepository.Update(recipe);
        await _foodRepository.Update(food);

        return RecipeDto.FromEntity(recipe);
    }

    public async Task<RecipeDto> CreateRecipe(RecipeDto recipeDto)
    {
        Recipe recipe = new()
        {
            Name = recipeDto.Name
        };

        List<RecipeIngredient> recipeIngredients = [];

        foreach (IngredientDto ingredientDto in recipeDto.Ingredients)
        {
            if (ingredientDto.Quantity.UnitId == "")
            {
                ingredientDto.Quantity.UnitId = null;
            }

            Ingredient ingredient = await _ingredientRepository.FindOrCreateBy(ingredientDto.Name);

            RecipeIngredient recipeIngredient = new()
            {
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

        await _recipeRepository.Insert(recipe);

        return recipeDto;
    }

    public async Task DeleteRecipe(string id)
    {
        Recipe recipe = await _recipeRepository.Get(id)
            ?? throw new ApplicationException("recipe to delete not found");
    
        await _recipeRepository.Delete(recipe);
    }

    public async Task<RecipeDto?> GetRecipe(string id)
    {
        Recipe? recipe = await _recipeRepository.Get(id);

        return (recipe == null) ? null : RecipeDto.FromEntity(recipe);
    }

    public async Task<List<RecipeDto>> GetRecipes(RecipeSortOptions sortBy, string? searchName)
    {
        List<Recipe> recipes = await _recipeRepository.GetAll(sortBy, searchName);
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
    
        Recipe recipe = await _recipeRepository.Get(recipeDto.Id)
                                ?? throw new ApplicationException("recipe not found");

        recipe.Name = recipeDto.Name;

        List<RecipeIngredient> newRecipeIngredients = [];

        foreach(IngredientDto ingredientDto in recipeDto.Ingredients)
        {
            Ingredient ingredient = recipe.Ingredients
                                .FirstOrDefault(ingr => ingr.Name == ingredientDto.Name)
                                ?? await _ingredientRepository.FindOrCreateBy(ingredientDto.Name);

            RecipeIngredient newRecipeIngredient = new()
            {
                RecipeId = recipe.Id,
                IngredientId = ingredient.Id,
                Quantity = Quantity.FromDto(ingredientDto.Quantity)
            };
            newRecipeIngredients.Add(newRecipeIngredient);
        }

        recipe.RecipeIngredients = newRecipeIngredients;

        return RecipeDto.FromEntity(await _recipeRepository.Update(recipe));
    }
}
