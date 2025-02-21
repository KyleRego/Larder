using Larder.Services.Impl;
using Larder.Services.Interface;
using Larder.Tests.Mocks.Repository;

namespace Larder.Tests.Services.RecipeServiceTests;

public abstract class RecipeServiceTestsBase : ServiceTestsBase
{
    protected readonly MockUnitData _unitData = new();
    protected readonly MockUnitConversionData _unitConversionData;
    protected readonly MockIngredientData _ingredientData;
    protected readonly MockRecipeData _recipeData;
    protected readonly IUnitService _unitService;
    protected readonly IQuantityService _quantityService;
    protected readonly IUnitConversionService _unitConversionService;
    protected readonly IRecipeService _sut;
    public RecipeServiceTestsBase()
    {
        _unitConversionData = new(_unitData);
        _ingredientData = new(_unitData);
        _recipeData = new(_ingredientData, _unitData);
        _unitService = new UnitService(_serviceProvider.Object, _unitData);
        _unitConversionService = new UnitConversionService(
            _serviceProvider.Object, _unitData, _unitConversionData);
        _quantityService = new QuantityService(
            _serviceProvider.Object, _unitService, _unitConversionService);
        
        _sut = new RecipeService(
            _serviceProvider.Object,
            _recipeData,
            _ingredientData,
            _quantityService);
    }
}