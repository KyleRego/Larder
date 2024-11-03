using Larder.Models;
using Larder.Services;
using Larder.Repository;
using Larder.Dtos;

namespace Larder.Tests.Services;

public class RecipeServiceTests : ServiceTestsBase
{
    [Fact]
    public async void UpdateRecipeThrowsIfIdIsNull()
    {
        var recipeRepository = new Mock<IRecipeRepository>();
        var ingredientRepository = new Mock<IIngredientRepository>();
        var foodRepository = new Mock<IFoodRepository>();
        var unitConvRep = new Mock<IUnitConversionRepository>();

        RecipeService sut = new(mSP.Object, recipeRepository.Object,
                                    ingredientRepository.Object,
                                    foodRepository.Object, unitConvRep.Object);

        RecipeDto recipe = new()
        {
            Name = "New recipe",
            Ingredients = [
                new() {
                    Name = "Eggs",
                    Quantity = new() { Amount = 1, }
                }
            ]
        };

        await Assert.ThrowsAsync<ApplicationException>(
                async () => await sut.UpdateRecipe(recipe));
    }

    [Fact]
    public async void UpdateRecipeThrowsIfRecipeNotFound()
    {
        var recipeRepository = new Mock<IRecipeRepository>();
        var ingredientRepository = new Mock<IIngredientRepository>();
        var foodRepository = new Mock<IFoodRepository>();
        var unitConvRepository = new Mock<IUnitConversionRepository>();

        string id = "made_up_id";
        recipeRepository.Setup(r => r.Get(mockUserId, id)).ReturnsAsync((Recipe?)null);

        RecipeService sut = new(mSP.Object, recipeRepository.Object,
                                    ingredientRepository.Object,
                                    foodRepository.Object,
                                    unitConvRepository.Object);

        RecipeDto recipe = new()
        {
            Id = id,
            Name = "New recipe",
            Ingredients = [
                new() {
                    Name = "Eggs",
                    Quantity = new() { Amount = 1, }
                }
            ]
        };

        await Assert.ThrowsAsync<ApplicationException>(
            async () => await sut.UpdateRecipe(recipe));
    }

    [Fact]
    public async void CookRecipeDecreasesIngredientAmountsAndCreatesFood()
    {
        Recipe recipe = new(mockUserId, "Rice with butter");
        string recipeId = recipe.Id;

        Unit ing1Unit = new(mockUserId, "Cups", UnitType.Volume);

        Item ingItem1 = new(mockUserId, "White rice");

        Ingredient ing1 = new()
        {
            Item = ingItem1,
            Quantity = new() { Amount = 5, Unit = ing1Unit, UnitId = ing1Unit.Id }
        };
        ingItem1.Ingredient = ing1;

        Unit ing2Unit = new(mockUserId, "Tablespoons", UnitType.Volume);

        Item ingItem2 = new(mockUserId, "Butter");

        Ingredient ing2 = new()
        {
            Item = ingItem2,
            Quantity = new() { Amount = 12, Unit = ing2Unit, UnitId = ing2Unit.Id }
        };
        ingItem2.Ingredient = ing2;

        recipe.RecipeIngredients = [
            new(mockUserId, recipe.Id, ingItem1.Ingredient.Id,
                new() { Amount = 3, Unit = ing1Unit, UnitId = ing1Unit.Id })
            {
                Recipe = recipe,
                Ingredient = ingItem1.Ingredient
            },
            new(mockUserId, recipe.Id, ingItem2.Id,
                new() { Amount = 2, Unit = ing2Unit, UnitId = ing2Unit.Id })
            {
                Recipe = recipe,
                Ingredient = ingItem2.Ingredient
            }
        ];

        recipe.Ingredients = [ingItem1.Ingredient, ingItem2.Ingredient];

        var recipeRepository = new Mock<IRecipeRepository>();
        recipeRepository.Setup(_ => _.Get(mockUserId, recipeId)).ReturnsAsync(recipe);

        var ingredientRepository = new Mock<IIngredientRepository>();

        var foodRepository = new Mock<IFoodRepository>();

        Item foodItem = new(mockUserId, recipe.Name);

        Food food = new()
        {
            Item = foodItem,
            Servings = 1
        };
        foodItem.Food = food;

        foodRepository.Setup(_ => _.FindOrCreateBy(mockUserId, recipe.Name))
                        .ReturnsAsync(foodItem);

        var unitConvRep = new Mock<IUnitConversionRepository>();
    
        RecipeService sut = new(mSP.Object, recipeRepository.Object,
                                ingredientRepository.Object,
                                foodRepository.Object, unitConvRep.Object);

        CookRecipeDto dto = new()
        {
            RecipeId = recipe.Id
        };

        await sut.CookRecipe(dto);

        // Verify that the food servings was increased
        foodRepository.Verify(_ => _.Update(It.Is<Item>(item =>
            item.Name == recipe.Name && item.Food!.Servings == 1 + recipe.ServingsProduced
        )));

        // Verify that the ingredient quantities were decreased
        recipeRepository.Verify(_ => _.Update(It.Is<Recipe>(r => 
            r.Ingredients.First(ing => ing.Item.Name == ingItem1.Name).Quantity.Amount == 2
            && r.Ingredients.First(ing => ing.Item.Name == ingItem2.Name).Quantity.Amount == 10
        )));
    }

    [Fact]
    public async void CookRecipeConvertsRecipeIngredientQuantityToIngredientUnitToDecreaseIngredientQuantity()
    {
        Unit cupsUnit = new(mockUserId, "Cups",UnitType.Volume);
        Unit mlUnit = new(mockUserId,"ml",UnitType.Volume);

        Item ingItem = new(mockUserId, "Water");

        Ingredient ingredient = new() { Item = ingItem,
                                        Quantity = new() { Amount = 6, Unit = cupsUnit, UnitId = cupsUnit.Id } };
        ingItem.Ingredient = ingredient;

        UnitConversion conversion = new(mockUserId, cupsUnit.Id, mlUnit.Id, 237, UnitType.Volume);

        var recipeRepo = new Mock<IRecipeRepository>();
        Recipe recipe = new(mockUserId, "Test recipe");

        RecipeIngredient recipeIngredient = new(mockUserId, recipe.Id, ingredient.Id,
            new() { Amount=474, Unit=mlUnit, UnitId=mlUnit.Id})
        {
            Recipe = recipe,
            Ingredient = ingredient
        };

        recipe.RecipeIngredients = [recipeIngredient];
        recipe.Ingredients = [ingredient];
        recipeRepo.Setup(_ => _.Get(mockUserId, recipe.Id)).ReturnsAsync(recipe);

        var ingredientRepo = new Mock<IIngredientRepository>();

        var foodRepo = new Mock<IFoodRepository>();
        Item foodItem = new(mockUserId, recipe.Name);

        Food food = new(){ Item = foodItem };
        foodItem.Food = food;

        foodRepo.Setup(_ => _.FindOrCreateBy(mockUserId, recipe.Name)).ReturnsAsync(foodItem);

        var unitConvRepo = new Mock<IUnitConversionRepository>();
        unitConvRepo.Setup(_ => _.FindByUnitIdsEitherWay(mockUserId, cupsUnit.Id, mlUnit.Id)).ReturnsAsync(conversion);

        RecipeService sut = new(mSP.Object, recipeRepo.Object, ingredientRepo.Object,
                                 foodRepo.Object, unitConvRepo.Object);

        CookRecipeDto dto = new() { RecipeId = recipe.Id };

        CookRecipeResultDto result = await sut.CookRecipe(dto);
        IngredientDto ingrResult = result.Ingredients.First();
        Assert.Equal(4 , ingrResult.Quantity.Amount);
    }
}
