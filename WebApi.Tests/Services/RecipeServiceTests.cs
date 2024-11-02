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
        string recipeId = "1";
        Recipe recipe = new()
        {
            UserId = mockUserId,
            Id = recipeId,
            Name = "Rice with butter"
        };

        Unit ing1Unit = new()
        {
            UserId = mockUserId,
            Id = "unit1",
            Name = "Cups",
            Type = UnitType.Volume
        };

        Item ingItem1 = new()
        {
            UserId = mockUserId,
            Name = "White rice",
        };

        Ingredient ing1 = new()
        {
            Item = ingItem1,
            Quantity = new() { Amount = 5, Unit = ing1Unit, UnitId = ing1Unit.Id }
        };
        ingItem1.Ingredient = ing1;

        Unit ing2Unit = new()
        {
            UserId = mockUserId,
            Id = "unit2",
            Name = "Tablespoons",
            Type = UnitType.Volume
        };

        Item ingItem2 = new()
        {
            UserId = mockUserId,
            Name = "Butter"
        };

        Ingredient ing2 = new()
        {
            Item = ingItem2,
            Quantity = new() { Amount = 12, Unit = ing2Unit, UnitId = ing2Unit.Id }
        };
        ingItem2.Ingredient = ing2;

        recipe.RecipeIngredients = [
            new()
            {
                UserId = mockUserId,
                Ingredient = ingItem1.Ingredient,
                RecipeId = recipe.Id,
                IngredientId = ingItem1.Id,
                Quantity = new() { Amount = 3, Unit = ing1Unit, UnitId = ing1Unit.Id }
            },
            new()
            {
                UserId = mockUserId,
                Ingredient = ingItem2.Ingredient,
                RecipeId = recipe.Id,
                IngredientId = ingItem2.Id,
                Quantity = new() { Amount = 2, Unit = ing2Unit, UnitId = ing2Unit.Id }
            }
        ];

        recipe.Ingredients = [ingItem1.Ingredient, ingItem2.Ingredient];

        var recipeRepository = new Mock<IRecipeRepository>();
        recipeRepository.Setup(_ => _.Get(mockUserId, recipeId)).ReturnsAsync(recipe);

        var ingredientRepository = new Mock<IIngredientRepository>();

        var foodRepository = new Mock<IFoodRepository>();

        Item foodItem = new()
        {
            UserId = mockUserId,
            Name = recipe.Name
        };
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
        Unit cupsUnit = new() { UserId = mockUserId, Name="Cups", Type=UnitType.Volume };
        Unit mlUnit = new() { UserId = mockUserId, Name="ml", Type=UnitType.Volume };

        Item ingItem = new()
        {
            Id = "ingredient1",
            Name = "Water",
            UserId = mockUserId
        };
        Ingredient ingredient = new() { Item = ingItem,
                                        Quantity = new() { Amount = 6, Unit = cupsUnit, UnitId = cupsUnit.Id } };
        ingItem.Ingredient = ingredient;

        UnitConversion conversion = new()
        {
            UserId = mockUserId,
            UnitId = cupsUnit.Id,
            Unit = cupsUnit,
            TargetUnitId = mlUnit.Id,
            TargetUnit = mlUnit,
            TargetUnitsPerUnit = 237
        };

        var recipeRepo = new Mock<IRecipeRepository>();
        string recipeId = "1";
        Recipe recipe = new()
        {
            UserId = mockUserId,
            Id = recipeId, 
            Name = "Test Recipe"
        };

        RecipeIngredient recipeIngredient = new()
        {
            UserId = mockUserId,
            RecipeId = recipeId,
            IngredientId = ingredient.Id,
            Ingredient = ingredient,
            // 474 ml is 2 cups
            Quantity = new() { Amount=474, Unit=mlUnit, UnitId=mlUnit.Id}
        };

        recipe.RecipeIngredients = [recipeIngredient];
        recipe.Ingredients = [ingredient];
        recipeRepo.Setup(_ => _.Get(mockUserId, recipeId)).ReturnsAsync(recipe);

        var ingredientRepo = new Mock<IIngredientRepository>();

        var foodRepo = new Mock<IFoodRepository>();
        Item foodItem = new()
        {
            Name = recipe.Name,
            UserId = mockUserId
        };

        Food food = new(){ Item = foodItem };
        foodItem.Food = food;

        foodRepo.Setup(_ => _.FindOrCreateBy(mockUserId, recipe.Name)).ReturnsAsync(foodItem);

        var unitConvRepo = new Mock<IUnitConversionRepository>();
        unitConvRepo.Setup(_ => _.FindByUnitIdsEitherWay(mockUserId, cupsUnit.Id, mlUnit.Id)).ReturnsAsync(conversion);

        RecipeService sut = new(mSP.Object, recipeRepo.Object, ingredientRepo.Object,
                                 foodRepo.Object, unitConvRepo.Object);

        CookRecipeDto dto = new() { RecipeId = recipeId };

        CookRecipeResultDto result = await sut.CookRecipe(dto);
        IngredientDto ingrResult = result.Ingredients.First();
        Assert.Equal(4 , ingrResult.Quantity.Amount);
    }
}
