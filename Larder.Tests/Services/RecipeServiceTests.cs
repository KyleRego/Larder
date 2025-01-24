using Larder.Models;
using Larder.Services;
using Larder.Repository;
using Larder.Dtos;
using Larder.Models.ItemComponent;
using Larder.Services.Interface;
using Larder.Services.Impl;

namespace Larder.Tests.Services;

public class RecipeServiceTests : ServiceTestsBase
{
    [Fact]
    public async void UpdateRecipeThrowsIfIdIsNull()
    {
        var recipeRepository = new Mock<IRecipeRepository>();
        var ingredientRepository = new Mock<IIngredientRepository>();
        var foodRepository = new Mock<IFoodRepository>();
        var quantityMathService = new Mock<IQuantityMathService>();

        RecipeService sut = new(mSP.Object, recipeRepository.Object,
                                    ingredientRepository.Object,
                                    foodRepository.Object,
                                    quantityMathService.Object);

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
        var quantityMathService = new Mock<IQuantityMathService>();

        string id = "made_up_id";
        recipeRepository.Setup(r => r.Get(mockUserId, id)).ReturnsAsync((Recipe?)null);

        RecipeService sut = new(mSP.Object, recipeRepository.Object,
                                    ingredientRepository.Object,
                                    foodRepository.Object,
                                    quantityMathService.Object);

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

    // [Fact]
    // public async void CookRecipeDecreasesIngredientAmountsAndCreatesFood()
    // {
    //     Recipe recipe = new(mockUserId, "Rice with butter");
    //     string recipeId = recipe.Id;

    //     Unit ing1Unit = new(mockUserId, "Cups", UnitType.Volume);
    //     Item ingItem1 = new(mockUserId, "White rice", 1);
    //     QuantityComponent ingItem1Quantity =  new()
    //     {
    //         Item = ingItem1,
    //         Quantity = new() { Amount = 5, Unit = ing1Unit, UnitId = ing1Unit.Id }
    //     };
    //     ingItem1.QuantityComp = ingItem1Quantity;
    //     Ingredient ing1 = new()
    //     {
    //         Item = ingItem1
    //     };
    //     ingItem1.Ingredient = ing1;

    //     Unit ing2Unit = new(mockUserId, "Tablespoons", UnitType.Volume);
    //     Item ingItem2 = new(mockUserId, "Butter", 1);
    //     QuantityComponent ingItem2Quantity = new()
    //     {
    //         Item = ingItem2,
    //         Quantity = new() { Amount = 12, Unit = ing2Unit, UnitId = ing2Unit.Id }
    //     };
    //     ingItem2.QuantityComp = ingItem2Quantity;
    //     Ingredient ing2 = new()
    //     {
    //         Item = ingItem2
    //     };
    //     ingItem2.Ingredient = ing2;

    //     recipe.RecipeIngredients = [
    //         new(mockUserId, recipe.Id, ingItem1.Ingredient.Id)
    //         {
    //             Recipe = recipe,
    //             Ingredient = ingItem1.Ingredient,
    //             Quantity = new() { Amount = 3, Unit = ing1Unit, UnitId = ing1Unit.Id }
    //         },
    //         new(mockUserId, recipe.Id, ingItem2.Id)
    //         {
    //             Recipe = recipe,
    //             Ingredient = ingItem2.Ingredient,
    //             Quantity = new() { Amount = 2, Unit = ing2Unit, UnitId = ing2Unit.Id }
    //         }
    //     ];

    //     recipe.Ingredients = [ingItem1.Ingredient, ingItem2.Ingredient];

    //     var recipeRepository = new Mock<IRecipeRepository>();
    //     recipeRepository.Setup(_ => _.Get(mockUserId, recipeId)).ReturnsAsync(recipe);

    //     var ingredientRepository = new Mock<IIngredientRepository>();

    //     var foodRepository = new Mock<IFoodRepository>();

    //     Item foodItem = new(mockUserId, recipe.Name, 1);

    //     Food food = new()
    //     {
    //         Item = foodItem,
    //         Servings = 1
    //     };
    //     foodItem.Food = food;

    //     foodRepository.Setup(_ => _.FindOrCreateBy(mockUserId, recipe.Name))
    //                     .ReturnsAsync(foodItem);

    //     var unitConvRep = new Mock<IUnitConversionRepository>();
    //     UnitConversionService unitConversionService = new(mSP.Object, unitConvRep.Object);
    //     QuantityMathService quantityMathService = new(mSP.Object, unitConversionService);
    
    //     RecipeService sut = new(mSP.Object, recipeRepository.Object,
    //                             ingredientRepository.Object,
    //                             foodRepository.Object, quantityMathService.Object);

    //     CookRecipeDto dto = new()
    //     {
    //         RecipeId = recipe.Id
    //     };

    //     await sut.CookRecipe(dto);

    //     // Verify that the food servings was increased
    //     foodRepository.Verify(_ => _.Update(It.Is<Item>(item =>
    //         item.Name == recipe.Name && item.Food!.Servings == 1 + recipe.ServingsProduced
    //     )));

    //     // Verify that the ingredient quantities were decreased
    //     recipeRepository.Verify(_ => _.Update(It.Is<Recipe>(r => 
    //         r.Ingredients.First(ing => ing.Item.Name == ingItem1.Name).Item.QuantityComp!.Quantity.Amount == 2
    //         && r.Ingredients.First(ing => ing.Item.Name == ingItem2.Name).Item.QuantityComp!.Quantity.Amount == 10
    //     )));
    // }

    // [Fact]
    // public void CookRecipeConvertsRecipeIngredientQuantityToIngredientUnitToDecreaseIngredientQuantity()
    // {
    //     Unit cupsUnit = new(mockUserId, "Cups",UnitType.Volume);
    //     Unit mlUnit = new(mockUserId,"ml",UnitType.Volume);

    //     Item ingItem = new(mockUserId, "Water", 1);
    //     QuantityComponent quantityComponent = new()
    //     {
    //         Item = ingItem,
    //         Quantity = new() { Amount = 6, Unit = cupsUnit, UnitId = cupsUnit.Id }
    //     };
    //     ingItem.QuantityComp = quantityComponent;

    //     Ingredient ingredient = new() { Item = ingItem };
    //     ingItem.Ingredient = ingredient;

    //     UnitConversion conversion = new(mockUserId, cupsUnit.Id, mlUnit.Id, 237)
    //     {
    //         UnitType = UnitType.Volume
    //     };

    //     var recipeRepository = new Mock<IRecipeRepository>();
    //     Recipe recipe = new(mockUserId, "Test recipe");

    //     RecipeIngredient recipeIngredient = new(mockUserId, recipe.Id, ingredient.Id)
    //     {
    //         Recipe = recipe,
    //         Ingredient = ingredient,
    //         Quantity = new() { Amount=474, Unit=mlUnit, UnitId=mlUnit.Id}
    //     };

    //     recipe.RecipeIngredients = [recipeIngredient];
    //     recipe.Ingredients = [ingredient];
    //     recipeRepository.Setup(_ => _.Get(mockUserId, recipe.Id)).ReturnsAsync(recipe);

    //     var ingredientRepo = new Mock<IIngredientRepository>();

    //     var foodRepo = new Mock<IFoodRepository>();
    //     Item foodItem = new(mockUserId, recipe.Name, 1);

    //     Food food = new(){ Item = foodItem };
    //     foodItem.Food = food;

    //     foodRepo.Setup(_ => _.FindOrCreateBy(mockUserId, recipe.Name)).ReturnsAsync(foodItem);

    //     var quantityMathService = new Mock<IQuantityMathService>();

    //     RecipeService sut = new(mSP.Object, recipeRepository.Object, ingredientRepo.Object,
    //                              foodRepo.Object, quantityMathService.Object);

    //     CookRecipeDto dto = new() { RecipeId = recipe.Id };

    //     // IngredientDto ingrResult = result.Ingredients.First();
    //     // Assert.Equal(4 , ingrResult.ItemDto.Quantity.Amount);
    //     recipeRepository.Verify(_ => _.Update(It.Is<Recipe>(r => 
    //         r.Ingredients.First().Item.QuantityComp!.Quantity.Amount == 4
    //     )));
    // }
}
