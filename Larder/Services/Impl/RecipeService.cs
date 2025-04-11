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
                                    IItemService itemService,
                                    IQuantityService quantityService)
    : CrudServiceBase<RecipeDto, Recipe>(serviceProvider, recipeData), IRecipeService
{
    private readonly IRecipeRepository _recipeData = recipeData;
    private readonly IItemService _itemService = itemService;
    private readonly IQuantityService _quantityService = quantityService;

    public async Task<ItemDto> CookRecipe(CookRecipeDto cookRecipeDto)
    {
        Recipe recipe = await _recipeData.Get(CurrentUserId(),
                                            cookRecipeDto.RecipeId);

        double foodServingsMade = cookRecipeDto.ServingsProduced;
        ItemBuilder cookedFoodBuilder = new ItemBuilder(CurrentUserId(), recipe.Name)
                            .WithQuantity(foodServingsMade);
        NutritionBuilder nutritionBuilder = new NutritionBuilder()
                            .WithServingSize(1);

        foreach (CookRecipeIngredientDto cookedIngredient in cookRecipeDto.Ingredients)
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

            if (nutrition.ServingSize.Amount == 0)
                throw new ApplicationException(
                    $"Item with ID {cookedItemId} has a serving size amount of 0; this cannot be used in division"
                );

            double ingredientServingsCooked = await _quantityService.Divide(
                        quantityCooked,
                        (QuantityDto)nutrition.ServingSize);

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

        ItemDto newFood = cookedFoodBuilder.WithNutrition(nutritionBuilder).BuildDto();
        ItemDto insertedFood = await _itemService.Add(newFood);

        await _recipeData.Update(recipe);
        return insertedFood;
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

    protected override RecipeDto MapToDto(Recipe recipe)
    {
        RecipeDto recipeDto = new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Ingredients = []
        };

        foreach (RecipeIngredient recipeIngredient
                        in recipe.RecipeIngredients)
        {
            RecipeIngredientDto riDto = new()
            {
                Id = recipeIngredient.Id,
                Name = recipeIngredient.Ingredient.Name,
                Quantity = (QuantityDto)recipeIngredient.DefaultQuantity
            };

            recipeDto.Ingredients.Add(riDto);
        }

        return recipeDto;
    }

    protected async override Task<Recipe> MapToEntity(RecipeDto recipeDto)
    {
        Recipe recipe = new(CurrentUserId(), recipeDto.Name);

        List<RecipeIngredient> recipeIngredients = [];

        foreach (RecipeIngredientDto ingredientDto in recipeDto.Ingredients)
        {
            if (ingredientDto.Quantity.UnitId == "")
            {
                ingredientDto.Quantity.UnitId = null;
            }

            ItemDto ingItem = await _itemService.FindOrCreate(ingredientDto.Name);

            Quantity quantity = new()
            {
                Amount = ingredientDto.Quantity.Amount,
                UnitId = ingredientDto.Quantity.UnitId
            };

            RecipeIngredient recipeIngredient = new(CurrentUserId(), recipe.Id, ingItem.Id!)
            {
                DefaultQuantity = quantity
            };

            recipeIngredients.Add(recipeIngredient);
        }

        recipe.RecipeIngredients = recipeIngredients;

        return recipe;
    }
}
