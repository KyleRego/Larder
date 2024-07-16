using Larder.Dtos;
using Larder.Models;
using Larder.Repository;

namespace Larder.Services;

public interface IRecipeService
{
    public Task<RecipeDto?> GetRecipe(string recipeId);

    public Task<List<RecipeDto>> GetRecipes();

    public Task<RecipeDto?> UpdateRecipe(RecipeDto recipeDto);
}

public class RecipeService( IRecipeRepository recipeRepository,
                            IIngredientRepository ingredientRepository) : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IIngredientRepository _ingredientRepository = ingredientRepository;

    public async Task<RecipeDto?> GetRecipe(string recipeId)
    {
        Recipe? recipe = await _recipeRepository.GetRecipe(recipeId);

        if (recipe == null) return null;

        return RecipeDtoAssembler.Assemble(recipe);
    }

    public async Task<List<RecipeDto>> GetRecipes()
    {
        List<Recipe> recipes = await _recipeRepository.GetRecipes();
        List<RecipeDto> recipeDtos = [];

        foreach (Recipe recipe in recipes)
        {
            recipeDtos.Add(RecipeDtoAssembler.Assemble(recipe));
        }

        return recipeDtos;
    }

    public async Task<RecipeDto?> UpdateRecipe(RecipeDto recipeDto)
    {
        // TODO: Rather than returning null, throw 
        // exception types specific to what happened
        // so controller can return appropriate responses
        string? recipeId = recipeDto.RecipeId;
        if (recipeId == null) { return null; }

        Recipe? recipe = await _recipeRepository.GetRecipe(recipeId);
        if (recipe == null) { return null; }

        recipe.Name = recipeDto.RecipeName;

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
            string? unitId = recipeIngredientDto.UnitId;

            if (recipeIngredient == null)
            {
                recipeIngredient = new()
                {
                    RecipeId = recipe.Id,
                    IngredientId = ingredient.Id,
                    Amount = amount,
                    UnitId = unitId
                };
                recipe.RecipeIngredients.Add(recipeIngredient);
            }
            else
            {
                recipeIngredient.IngredientId = ingredient.Id;
                recipeIngredient.Amount = amount;
                recipeIngredient.UnitId = unitId;
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

        return RecipeDtoAssembler.Assemble(await _recipeRepository.UpdateRecipe(recipe));
    }
}