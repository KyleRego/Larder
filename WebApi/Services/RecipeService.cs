using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IRecipeService
{
    public Task<RecipeDto?> GetRecipe(string id);

    public Task<List<RecipeDto>> GetRecipes(RecipeSortOptions sortBy, string? searchName);

    public Task<RecipeDto> CreateRecipe(RecipeDto recipeDto);

    public Task<RecipeDto> UpdateRecipe(RecipeDto recipeDto);
}

public class RecipeService( IRecipeRepository recipeRepository,
                            IIngredientRepository ingredientRepository) : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IIngredientRepository _ingredientRepository = ingredientRepository;

    public async Task<RecipeDto> CreateRecipe(RecipeDto recipeDto)
    {
        string name = recipeDto.Name;

        Food food = new()
        {
            Name = name,
        };

        Recipe recipe = new()
        {
            Food = food,
            FoodId = food.Id,
            Name = recipeDto.Name
        };

        List<RecipeIngredient> recipeIngredients = [];

        foreach (RecipeIngredientDto ingredientDto in recipeDto.Ingredients)
        {
            Ingredient ingredient = await _ingredientRepository.FindOrCreateBy(ingredientDto.IngredientName);

            RecipeIngredient recipeIngredient = new()
            {
                RecipeId = recipe.Id,
                IngredientId = ingredient.Id
            };

            if (ingredientDto.Quantity != null)
            {
                recipeIngredient.Quantity = new()
                {
                    Amount = ingredientDto.Quantity.Amount,
                    UnitId = ingredientDto.Quantity.UnitId
                };
            }

            recipeIngredients.Add(recipeIngredient);
        }

        recipe.RecipeIngredients = recipeIngredients;

        await _recipeRepository.Insert(recipe);

        return recipeDto;
    }

    public async Task<RecipeDto?> GetRecipe(string id)
    {
        Recipe? recipe = await _recipeRepository.Get(id);

        if (recipe == null) return null;

        return RecipeDtoAssembler.Assemble(recipe);
    }

    public async Task<List<RecipeDto>> GetRecipes(RecipeSortOptions sortBy, string? searchName)
    {
        List<Recipe> recipes = await _recipeRepository.GetAll(sortBy, searchName);
        List<RecipeDto> recipeDtos = [];

        foreach (Recipe recipe in recipes)
        {
            recipeDtos.Add(RecipeDtoAssembler.Assemble(recipe));
        }

        return recipeDtos;
    }

    public async Task<RecipeDto> UpdateRecipe(RecipeDto recipeDto)
    {
        if (recipeDto.Id == null) throw new ApplicationException("recipe Id was missing");
    
        Recipe? recipe = await _recipeRepository.Get(recipeDto.Id) ?? throw new ApplicationException("recipe not found");

        string name = recipeDto.Name;

        recipe.Name = name;

        RecipeIngredient? FindRecipeIngredient(string? recipeIngredientId, List<RecipeIngredient> recipeIngredients)
        {
            if (recipeIngredientId == null) return null;

            return recipeIngredients.FirstOrDefault(ri => ri.Id == recipeIngredientId);
        }

        List<string> currentRecipeIngredientIds = [];

        foreach (RecipeIngredientDto recipeIngredientDto in recipeDto.Ingredients)
        {
            RecipeIngredient? recipeIngredient = FindRecipeIngredient(recipeIngredientDto.RecipeIngredientId,
                                                                        recipe.RecipeIngredients);

            string ingredientName = recipeIngredientDto.IngredientName;
            Ingredient ingredient = await _ingredientRepository.FindOrCreateBy(ingredientName);

            if (recipeIngredient == null)
            {
                recipeIngredient = new()
                {
                    RecipeId = recipe.Id,
                    IngredientId = ingredient.Id,
                };
                recipe.RecipeIngredients.Add(recipeIngredient);
            }
            else
            {
                recipeIngredient.IngredientId = ingredient.Id;
            }

            if (recipeIngredientDto.Quantity != null)
            {
                recipeIngredient.Quantity = new()
                {
                    Amount = recipeIngredientDto.Quantity.Amount,
                    UnitId = recipeIngredientDto.Quantity.UnitId
                };
            }

            currentRecipeIngredientIds.Add(recipeIngredient.Id);
        }

        List<RecipeIngredient> resultingRecipeIngredients = [];

        foreach(RecipeIngredient recipeIngredient in recipe.RecipeIngredients)
        {
            if (currentRecipeIngredientIds.Contains(recipeIngredient.Id))
            {
                resultingRecipeIngredients.Add(recipeIngredient);
            }
        }

        recipe.RecipeIngredients = resultingRecipeIngredients;

        return RecipeDtoAssembler.Assemble(await _recipeRepository.Update(recipe));
    }
}