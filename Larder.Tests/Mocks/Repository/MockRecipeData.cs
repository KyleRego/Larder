using Larder.Models;
using Larder.Models.Builders;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Mocks.Repository;

public class MockRecipeData 
    : MockRepositoryBase, IRecipeRepository
{
    private readonly MockIngredientData _ingredientData;
    private readonly MockUnitData _unitData;
    private readonly List<Recipe> _recipes;

    public MockRecipeData(MockIngredientData ingredientData,
                            MockUnitData unitData)
    {
        _ingredientData = ingredientData;
        _unitData = unitData;
        _recipes = [];

        Unit tablespoons = _unitData.Get(testUserId, "tablespoons")
                            .GetAwaiter().GetResult()!;
        Unit grams = _unitData.Get(testUserId, "grams")
                            .GetAwaiter().GetResult()!;

        Item butter = _ingredientData.Get(testUserId, "butter")
                            .GetAwaiter().GetResult()!;
        Item boxRice = _ingredientData.Get(testUserId, "box-rice")
                            .GetAwaiter().GetResult()!;
        Item chickenLegQuarters = _ingredientData
                            .Get(testUserId, "chicken-leg-quarters")
                            .GetAwaiter().GetResult()!;

        Recipe chickenAndRice = new RecipeBuilder(testUserId, "Chicken and rice")
                            .WithId("chicken-and-rice")
                            .WithIngredient(butter, 4, tablespoons)
                            .WithIngredient(boxRice, 7 * 56, grams)
                            .WithIngredient(chickenLegQuarters, 4)
                            .Build();

        _recipes.Add(chickenAndRice);
                                
    }

    public Task Delete(Recipe entity)
    {
        throw new NotImplementedException();
    }

    public Task<Recipe?> Get(string userId, string id)
    {
        return Task.FromResult(
            _recipes.FirstOrDefault(recipe =>
                recipe.UserId == userId && id == recipe.Id));
    }

    public Task<List<Recipe>> GetAll(string userId, RecipeSortOptions sortOption = RecipeSortOptions.AnyOrder, string? search = null)
    {
        throw new NotImplementedException();
    }

    public Task<Recipe> Insert(Recipe newEntity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Recipe>> InsertAll(List<Recipe> newEntities)
    {
        throw new NotImplementedException();
    }

    public Task<Recipe> Update(Recipe editedEntity)
    {
        return Task.FromResult(editedEntity);
    }
}
