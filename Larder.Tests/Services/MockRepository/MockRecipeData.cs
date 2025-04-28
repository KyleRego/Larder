using Larder.Models;
using Larder.Models.Builders;
using Larder.Models.SortOptions;
using Larder.Repository.Interface;

namespace Larder.Tests.Services.MockRepository;

public class MockRecipeData : MockCrudRepositoryBase<Recipe>, IRecipeRepository
{
    private readonly IItemRepository _itemData;
    private readonly IUnitRepository _unitData;

    public MockRecipeData(IItemRepository itemData,
                            IUnitRepository unitData)
    {
        _itemData = itemData;
        _unitData = unitData;

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

        _records.Add(chickenAndRice);
                                
    }

    public Task<List<Recipe>> GetAll(string userId, RecipeSortOptions sortOption = RecipeSortOptions.AnyOrder, string? search = null)
    {
        throw new NotImplementedException();
    }
}
