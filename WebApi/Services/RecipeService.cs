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
    public Task<RecipeDto> CookRecipe(CookRecipeDto cookedRecipeDto);
}

public class RecipeService( IRecipeRepository recipeRepository,
                            IIngredientRepository ingredientRepository,
                            IFoodRepository foodRepository) : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;
    private readonly IIngredientRepository _ingredientRepository = ingredientRepository;
    private readonly IFoodRepository _foodRepository = foodRepository;

    public async Task<RecipeDto> CookRecipe(CookRecipeDto cookedRecipeDto)
    {
        Recipe recipe = await _recipeRepository.Get(cookedRecipeDto.RecipeId)
                ?? throw new ApplicationException("recipe was not found");

        foreach(RecipeIngredient recipeIngredient in recipe.RecipeIngredients)
        {
            Ingredient ingredient = recipeIngredient.Ingredient;

            // what if the quantities are in different units????
            // need to have a think about what the design should be regarding this
            ingredient.Quantity.Amount -= recipeIngredient.Quantity.Amount;
        }

        await _recipeRepository.Update(recipe);

        Food food = await _foodRepository.FindOrCreateBy(recipe.Name);
        food.Servings += recipe.ServingsProduced;
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
