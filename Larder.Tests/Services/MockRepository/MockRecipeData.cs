using Larder.Models;
using Larder.Models.Builders;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Services.MockRepository;

public class MockRecipeData 
    : MockRepositoryBase, IRecipeRepository
{
    private readonly IItemRepository _itemData;
    private readonly IUnitRepository _unitData;
    private readonly List<Recipe> _recipes;

    public MockRecipeData(IItemRepository itemData,
                            IUnitRepository unitData)
    {
        _itemData = itemData;
        _unitData = unitData;
        _recipes = [];

        Unit tablespoons = Helpers.Untask(
            _unitData.Get(testUserId, "tablespoons"));
        Unit grams = Helpers.Untask(
            _unitData.Get(testUserId, "grams"));

        Item butter = Helpers.Untask(
            _itemData.Get(testUserId, "butter"));      
        Item boxRice = Helpers.Untask(
            _itemData.Get(testUserId, "box-rice"));
        Item chickenLegQuarters = Helpers.Untask(
                _itemData.Get(testUserId, "chicken-leg-quarters"));

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
