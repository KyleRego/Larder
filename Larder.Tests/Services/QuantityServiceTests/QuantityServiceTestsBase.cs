using Larder.Services.Impl;
using Larder.Services.Interface;
using Larder.Tests.Services.MockRepository;

namespace Larder.Tests.Services.QuantityServiceTests;

public abstract class QuantityServiceTests : ServiceTestsBase
{
    protected readonly MockUnitData _unitData = new();
    protected readonly MockUnitConversionData _unitConversionData;
    protected readonly IUnitService _unitService;
    protected readonly IUnitConversionService _unitConversionService;
    protected readonly IQuantityService _sut;

    public QuantityServiceTests()
    {
        _unitConversionData = new(_unitData);
        _unitService = new UnitService(_serviceProvider.Object, _unitData);
        _unitConversionService = new UnitConversionService(
            _serviceProvider.Object, _unitService, _unitConversionData);
        _sut = new QuantityService(
            _serviceProvider.Object, _unitService, _unitConversionService);
    }
}
