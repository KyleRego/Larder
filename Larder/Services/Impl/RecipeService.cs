using Larder.Dtos;
using Larder.Models;
using Larder.Models.Builders;
using Larder.Models.ItemComponents;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;
using Larder.Services.Interface;

namespace Larder.Services.Impl;

public class RecipeService(IServiceProviderWrapper serviceProvider,
                                    IRecipeRepository recipeData,
                                    IIngredientRepository ingredientData,
                                    IQuantityService quantityService)
                     : AppServiceBase(serviceProvider), IRecipeService
{
    private readonly IRecipeRepository _recipeData = recipeData;
    private readonly IIngredientRepository _ingredientData = ingredientData;
    private readonly IQuantityService _quantityService = quantityService;

    public async Task<ItemDto> CookRecipe(CookRecipeDto cookRecipeDto)
    {
        Recipe recipe = await _recipeData.Get(CurrentUserId(),
                                            cookRecipeDto.RecipeId)
            ?? throw new ApplicationException(
                $"Recipe with ID {cookRecipeDto.RecipeId} not found");

        double foodServingsMade = cookRecipeDto.ServingsProduced;
        ItemBuilder cookedFoodBuilder = new ItemBuilder(CurrentUserId(), recipe.Name)
                            .WithQuantity(foodServingsMade);
        NutritionBuilder nutritionBuilder = new NutritionBuilder()
                            .WithServingSize(1);

        foreach(CookRecipeIngredientDto cookedIngredient in cookRecipeDto.Ingredients)
        {
            string cookedItemId = cookedIngredient.IngredientItemId;

            Item ingredientItem = recipe.Ingredients
                .FirstOrDefault(item => item.Id == cookedItemId)
                ?? throw new ApplicationException(
                $"Recipe is missing an ingredient item with ID ${cookedItemId}"
            );

            if (ingredientItem.Nutrition == null)
            {
                throw new ApplicationException(
                    $"Ingredient item with ID ${cookedItemId} has no Nutrition component"
                );
            }

            QuantityDto quantityCooked = await _quantityService.SubtractUpToZero(
                                (QuantityDto)ingredientItem.Quantity,
                                    cookedIngredient.QuantityCooked);

            QuantityDto quantityRemaining = await _quantityService.Subtract(
                                (QuantityDto)ingredientItem.Quantity,
                                    quantityCooked);

            ingredientItem.Quantity = Quantity.FromDto(quantityRemaining);

            Nutrition nutrition = ingredientItem.Nutrition;
            double ingredientServingsCooked;
            try
            {
                ingredientServingsCooked = await _quantityService.Divide(
                        quantityCooked,
                        (QuantityDto)nutrition.ServingSize);
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(
                    $"{e.Message} - Does ingredient item with ID {cookedItemId} have a serving size?");
            }

            nutritionBuilder
                .WithCalories(nutrition.Calories * ingredientServingsCooked / foodServingsMade)
                .WithProtein(nutrition.GramsProtein * ingredientServingsCooked / foodServingsMade)
                .WithDietaryFiber(nutrition.GramsDietaryFiber * ingredientServingsCooked / foodServingsMade)
                .WithSaturatedFat(nutrition.GramsSaturatedFat * ingredientServingsCooked / foodServingsMade)
                .WithTotalCarbs(nutrition.GramsTotalCarbs * ingredientServingsCooked / foodServingsMade)
                .WithTotalFat(nutrition.GramsTotalFat * ingredientServingsCooked / foodServingsMade)
                .WithTotalSugars(nutrition.GramsTotalSugars * ingredientServingsCooked / foodServingsMade)
                .WithTransFat(nutrition.GramsTransFat * ingredientServingsCooked / foodServingsMade)
                .WithCholesterol(nutrition.MilligramsCholesterol * ingredientServingsCooked / foodServingsMade)
                .WithSodium(nutrition.MilligramsSodium * ingredientServingsCooked / foodServingsMade);
        }

        Item newFood = cookedFoodBuilder.WithNutrition(nutritionBuilder).Build();
        Item insertedFood = await _ingredientData.Insert(newFood);

        await _recipeData.Update(recipe);
        return ItemDto.FromEntity(insertedFood);
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

            Item ingItem = await _ingredientData.FindOrCreate(
                                CurrentUserId(), ingredientDto.Name);
            ArgumentNullException.ThrowIfNull(ingItem.Ingredient);

            Quantity quantity = new()
            {
                Amount = ingredientDto.Quantity.Amount,
                UnitId = ingredientDto.Quantity.UnitId
            };
        
            RecipeIngredient recipeIngredient = new(CurrentUserId(), recipe.Id, ingItem.Id)
            {
                DefaultQuantity = quantity
            };

            recipeIngredients.Add(recipeIngredient);
        }

        recipe.RecipeIngredients = recipeIngredients;

        await _recipeData.Insert(recipe);

        return recipeDto;
    }

    public async Task DeleteRecipe(string id)
    {
        Recipe recipe = await _recipeData.Get(CurrentUserId(), id)
            ?? throw new ApplicationException("recipe to delete not found");
    
        await _recipeData.Delete(recipe);
    }

    public async Task<RecipeDto?> GetRecipe(string id)
    {
        Recipe? recipe = await _recipeData.Get(CurrentUserId(), id);

        if (recipe == null) return null;

        return RecipeDto.FromEntity(recipe);
    }

    public async Task<List<RecipeDto>> GetRecipes(RecipeSortOptions sortBy, string? searchName)
    {
        List<Recipe> recipes = await _recipeData.GetAll(CurrentUserId(), sortBy, searchName);
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
    
        Recipe recipe = await _recipeData.Get(CurrentUserId(), recipeDto.Id)
                    ?? throw new ApplicationException("recipe not found");

        recipe.Name = recipeDto.Name;

        List<RecipeIngredient> newRecipeIngredients = [];

        foreach(RecipeIngredientDto ingDto in recipeDto.Ingredients)
        {
            Item ingItem = recipe.Ingredients.FirstOrDefault(item =>
                                            item.Name == ingDto.Name)
                ?? await _ingredientData.FindOrCreate(CurrentUserId(), ingDto.Name);

            RecipeIngredient newRecipeIngredient
                            = new(CurrentUserId(), recipe.Id,ingItem.Id)
            {
                DefaultQuantity = Quantity.FromDto(ingDto.Quantity)
            };
            newRecipeIngredients.Add(newRecipeIngredient);
        }

        recipe.RecipeIngredients = newRecipeIngredients;

        return RecipeDto.FromEntity(await _recipeData.Update(recipe));
    }
}
