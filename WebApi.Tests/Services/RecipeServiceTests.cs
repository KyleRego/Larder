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

        RecipeService sut = new(recipeRepository.Object,
                                    ingredientRepository.Object,
                                    foodRepository.Object, unitConvRep.Object,
                                    mockHttpContextAccessor.Object,
                                    mockAuthorizationService.Object);

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
        recipeRepository.Setup(r => r.Get(id)).ReturnsAsync((Recipe?)null);

        RecipeService sut = new(recipeRepository.Object,
                                    ingredientRepository.Object,
                                    foodRepository.Object,
                                    unitConvRepository.Object,
                                    mockHttpContextAccessor.Object,
                                    mockAuthorizationService.Object);

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

        Unit ingredient1Unit = new()
        {
            UserId = mockUserId,
            Id = "unit1",
            Name = "Cups",
            Type = UnitType.Volume
        };

        Ingredient ingredient1 = new()
        {
            UserId = mockUserId,
            Name = "White rice",
            Quantity = new() { Amount = 5, Unit = ingredient1Unit, UnitId = ingredient1Unit.Id }
        };

        Unit ingredient2Unit = new()
        {
            UserId = mockUserId,
            Id = "unit2",
            Name = "Tablespoons",
            Type = UnitType.Volume
        };

        Ingredient ingredient2 = new()
        {
            UserId = mockUserId,
            Name = "Butter",
            Quantity = new() { Amount = 12, Unit = ingredient2Unit, UnitId = ingredient2Unit.Id }
        };

        recipe.RecipeIngredients = [
            new()
            {
                UserId = mockUserId,
                Ingredient = ingredient1,
                RecipeId = recipe.Id,
                IngredientId = ingredient1.Id,
                Quantity = new() { Amount = 3, Unit = ingredient1Unit, UnitId = ingredient1Unit.Id }
            },
            new()
            {
                UserId = mockUserId,
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
        foodRepository.Setup(_ => _.FindOrCreateBy(mockUserId, recipe.Name))
                        .ReturnsAsync(
            (Food)new() { UserId = mockUserId, Name = recipe.Name, Servings = 1 });

        var unitConvRep = new Mock<IUnitConversionRepository>();
    
        RecipeService sut = new(recipeRepository.Object,
                                ingredientRepository.Object,
                                foodRepository.Object, unitConvRep.Object,
                                mockHttpContextAccessor.Object,
                                mockAuthorizationService.Object);

        CookRecipeDto dto = new()
        {
            RecipeId = recipe.Id
        };

        await sut.CookRecipe(dto);

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
        Unit cupsUnit = new() { UserId = mockUserId, Name="Cups", Type=UnitType.Volume };
        Unit mlUnit = new() { UserId = mockUserId, Name="ml", Type=UnitType.Volume };
        Ingredient ingredient = new() { UserId = mockUserId, Id = "ingredient1", Name = "Water",
                                        Quantity = new() { Amount = 6, Unit = cupsUnit, UnitId = cupsUnit.Id } };
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
        recipeRepo.Setup(_ => _.Get(recipeId)).ReturnsAsync(recipe);

        var ingredientRepo = new Mock<IIngredientRepository>();

        var foodRepo = new Mock<IFoodRepository>();
        Food food = new() { UserId = mockUserId, Name = recipe.Name };
        foodRepo.Setup(_ => _.FindOrCreateBy(mockUserId, recipe.Name)).ReturnsAsync(food);

        var unitConvRepo = new Mock<IUnitConversionRepository>();
        unitConvRepo.Setup(_ => _.FindByUnitIdsEitherWay(mockUserId, cupsUnit.Id, mlUnit.Id)).ReturnsAsync(conversion);

        RecipeService sut = new(recipeRepo.Object, ingredientRepo.Object,
                                foodRepo.Object, unitConvRepo.Object,
                                mockHttpContextAccessor.Object,
                                mockAuthorizationService.Object);

        CookRecipeDto dto = new() { RecipeId = recipeId };

        CookRecipeResultDto result = await sut.CookRecipe(dto);
        IngredientDto ingrResult = result.Ingredients.First();
        Assert.Equal(4 , ingrResult.Quantity.Amount);
    }
}
