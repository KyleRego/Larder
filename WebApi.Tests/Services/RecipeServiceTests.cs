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

        RecipeService service = new(recipeRepository.Object,
                                    ingredientRepository.Object,
                                    foodRepository.Object);

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

        string id = "made_up_id";
        recipeRepository.Setup(r => r.Get(id)).ReturnsAsync((Recipe?)null);

        RecipeService service = new(recipeRepository.Object,
                                    ingredientRepository.Object,
                                    foodRepository.Object);

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
    
        RecipeService sut = new(recipeRepository.Object,
                                ingredientRepository.Object, foodRepository.Object);

        CookedRecipeDto dto = new()
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
}
