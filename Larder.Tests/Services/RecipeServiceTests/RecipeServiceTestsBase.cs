using Larder.Repository.Interface;
using Larder.Services.Impl;
using Larder.Services.Interface;
using Larder.Tests.Services.MockRepository;

namespace Larder.Tests.Services.RecipeServiceTests;

public abstract class RecipeServiceTestsBase : ServiceTestsBase
{
    protected readonly IUnitRepository _unitData = new MockUnitData();
    protected readonly IUnitConversionRepository _unitConversionData;
    protected readonly IItemRepository _itemData;
    protected readonly IItemService _itemService;
    protected readonly MockRecipeData _recipeData;
    protected readonly IUnitService _unitService;
    protected readonly IQuantityService _quantityService;
    protected readonly IUnitConversionService _unitConversionService;
    protected readonly IRecipeService _sut;
    public RecipeServiceTestsBase()
    {
        _unitConversionData = new MockUnitConversionData(_unitData);
        _itemData = new MockItemData(_unitData);
        _itemService = new ItemService(_serviceProvider.Object, _itemData);
        _recipeData = new(_itemData, _unitData);
        _unitService = new UnitService(_serviceProvider.Object, _unitData);
        _unitConversionService = new UnitConversionService(
            _serviceProvider.Object, _unitService, _unitConversionData);
        _quantityService = new QuantityService(
            _serviceProvider.Object, _unitService, _unitConversionService);
        
        _sut = new RecipeService(
            _serviceProvider.Object,
            _recipeData,
            _itemService,
            _quantityService);
    }
}