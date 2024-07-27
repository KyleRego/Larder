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
                IngredientId = ingredient.Id,
                Amount = ingredientDto.Amount
            };

            if (!string.IsNullOrWhiteSpace(ingredientDto.UnitId))
            {
                recipeIngredient.UnitId = ingredientDto.UnitId;
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
        if (recipeDto.Id == null)
        {
            throw new ApplicationException("Id was missing");
        }

        Recipe? recipe = await _recipeRepository.Get(recipeDto.Id);
        if (recipe == null)
        {
            throw new ApplicationException("Recipe was not found");
        }

        string name = recipeDto.Name;

        recipe.Name = name;
        recipe.Food.Name = name;

        RecipeIngredient? FindRecipeIngredient(string? recipeIngredientId, List<RecipeIngredient> recipeIngredients)
        {
            if (recipeIngredientId == null) return null;

            return recipeIngredients.FirstOrDefault(recIng => recIng.Id == recipeIngredientId);
        }

        List<string> currentRecipeIngredientIds = [];

        foreach (RecipeIngredientDto recipeIngredientDto in recipeDto.Ingredients)
        {
            RecipeIngredient? recipeIngredient = FindRecipeIngredient(recipeIngredientDto.RecipeIngredientId,
                                                                        recipe.RecipeIngredients);

            string ingredientName = recipeIngredientDto.IngredientName;
            Ingredient ingredient = await _ingredientRepository.FindOrCreateBy(ingredientName);
            double amount = recipeIngredientDto.Amount;

            if (recipeIngredient == null)
            {
                recipeIngredient = new()
                {
                    Id = recipe.Id,
                    IngredientId = ingredient.Id,
                    Amount = amount
                };
                recipe.RecipeIngredients.Add(recipeIngredient);
            }
            else
            {
                recipeIngredient.IngredientId = ingredient.Id;
                recipeIngredient.Amount = amount;
            }

            if (!string.IsNullOrWhiteSpace(recipeIngredientDto.UnitId))
            {
                recipeIngredient.UnitId = recipeIngredientDto.UnitId;
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