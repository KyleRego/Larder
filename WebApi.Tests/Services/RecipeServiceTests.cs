using Larder.Models;
using Larder.Services;
using Larder.Repository;
using Larder.Dtos;

namespace Larder.Tests.Services;

public class RecipeServiceTests
{
    [Fact]
    public async void UpdateRecipeThrowsIfIdIsNull()
    {
        var recipeRepository = new Mock<IRecipeRepository>();
        var ingredientRepository = new Mock<IIngredientRepository>();
        var foodRepository = new Mock<IFoodRepository>();
        var unitConvRep = new Mock<IUnitConversionRepository>();

        RecipeService service = new(recipeRepository.Object,
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

        await Assert.ThrowsAsync<ApplicationException>(async () => await service.UpdateRecipe(recipe));
    }

    [Fact]
    public async void UpdateRecipeThrowsIfRecipeNotFound()
    {
        var recipeRepository = new Mock<IRecipeRepository>();
        var ingredientRepository = new Mock<IIngredientRepository>();
        var foodRepository = new Mock<IFoodRepository>();
        var unitConvRepository = new Mock<IUnitConversionRepository>();

        string id = "made_up_id";
        recipeRepository.Setup(r => r.Get(id)).ReturnsAsync((Recipe?)null);

        RecipeService service = new(recipeRepository.Object,
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

        await Assert.ThrowsAsync<ApplicationException>(async () => await service.UpdateRecipe(recipe));
    }

    [Fact]
    public async void CookRecipeDecreasesIngredientAmountsAndCreatesFood()
    {
        // arrange
        string recipeId = "1";
        Recipe recipe = new()
        {
            Id = recipeId,
            Name = "Rice with butter"
        };

        Unit ingredient1Unit = new()
        {
            Id = "unit1",
            Name = "Cups",
            Type = UnitType.Volume
        };

        Ingredient ingredient1 = new()
        {
            Name = "White rice",
            Quantity = new() { Amount = 5, Unit = ingredient1Unit, UnitId = ingredient1Unit.Id }
        };

        Unit ingredient2Unit = new()
        {
            Id = "unit2",
            Name = "Tablespoons",
            Type = UnitType.Volume
        };

        Ingredient ingredient2 = new()
        {
            Name = "Butter",
            Quantity = new() { Amount = 12, Unit = ingredient2Unit, UnitId = ingredient2Unit.Id }
        };

        recipe.RecipeIngredients = [
            new()
            {
                Ingredient = ingredient1,
                RecipeId = recipe.Id,
                IngredientId = ingredient1.Id,
                Quantity = new() { Amount = 3, Unit = ingredient1Unit, UnitId = ingredient1Unit.Id }
            },
            new()
            {
                Ingredient = ingredient2,
                RecipeId = recipe.Id,
                IngredientId = ingredient2.Id,
                Quantity = new() { Amount = 2, Unit = ingredient2Unit, UnitId = ingredient2Unit.Id }
            }
        ];

        recipe.Ingredients = [ingredient1, ingredient2];

        var recipeRepository = new Mock<IRecipeRepository>();
        recipeRepository.Setup(_ => _.Get(recipeId)).ReturnsAsync(recipe);

        var ingredientRepository = new Mock<IIngredientRepository>();

        var foodRepository = new Mock<IFoodRepository>();
        foodRepository.Setup(_ => _.FindOrCreateBy(recipe.Name))
                                    .ReturnsAsync((Food)new() { Name = recipe.Name, Servings = 1 });

        var unitConvRep = new Mock<IUnitConversionRepository>();
    
        RecipeService sut = new(recipeRepository.Object,
                                ingredientRepository.Object,
                                foodRepository.Object, unitConvRep.Object);

        CookRecipeDto dto = new()
        {
            RecipeId = recipe.Id
        };

        // act  
        await sut.CookRecipe(dto);

        // assert
        // Verify that the food servings was increased
        foodRepository.Verify(_ => _.Update(It.Is<Food>(f =>
            f.Name == recipe.Name && f.Servings == 1 + recipe.ServingsProduced
        )));

        // Verify that the ingredient quantities were decreased
        recipeRepository.Verify(_ => _.Update(It.Is<Recipe>(r => 
            r.Ingredients.First(ingr => ingr.Name == ingredient1.Name).Quantity.Amount == 2
            && r.Ingredients.First(ingr => ingr.Name == ingredient2.Name).Quantity.Amount == 10
        )));
    }

    [Fact]
    public async void CookRecipeConvertsRecipeIngredientQuantityToIngredientUnitToDecreaseIngredientQuantity()
    {
        Unit cupsUnit = new() { Name="Cups", Type=UnitType.Volume };
        Unit mlUnit = new() { Name="ml", Type=UnitType.Volume };
        Ingredient ingredient = new() { Id = "ingredient1", Name = "Water",
                                        Quantity = new() { Amount = 6, Unit = cupsUnit, UnitId = cupsUnit.Id } };
        UnitConversion conversion = new()
        {
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
            Id = recipeId, 
            Name = "Test Recipe"
        };

        RecipeIngredient recipeIngredient = new()
        {
            RecipeId = recipeId,
            IngredientId = ingredient.Id,
            Ingredient = ingredient,
            // 474 ml is 2 cups
            Quantity = new() { Amount=474, Unit=mlUnit, UnitId=mlUnit.Id}
        };

        recipe.RecipeIngredients = [recipeIngredient];
        recipe.Ingredients = [ingredient];
        recipeRepo.Setup(_ => _.Get(recipeId)).ReturnsAsync(recipe);

        var ingredientRepo = new Mock<IIngredientRepository>();

        var foodRepo = new Mock<IFoodRepository>();
        Food food = new() { Name = recipe.Name };
        foodRepo.Setup(_ => _.FindOrCreateBy(recipe.Name)).ReturnsAsync(food);

        var unitConvRepo = new Mock<IUnitConversionRepository>();
        unitConvRepo.Setup(_ => _.FindByUnitIdsEitherWay(cupsUnit.Id, mlUnit.Id)).ReturnsAsync(conversion);

        RecipeService sut = new(recipeRepo.Object, ingredientRepo.Object,
                                foodRepo.Object, unitConvRepo.Object);

        CookRecipeDto dto = new() { RecipeId = recipeId };

        CookRecipeResultDto result = await sut.CookRecipe(dto);
        IngredientDto ingrResult = result.Ingredients.First();
        Assert.Equal(4 , ingrResult.Quantity.Amount);
    }
}
