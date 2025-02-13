using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Larder.Repository.Interface;
using Larder.Services.Impl;

namespace Larder.Tests.Services;

public class UnitConversionServiceTests : ServiceTestsBase
{
    private readonly IUnitRepository _mockUnitData;
    private readonly IUnitConversionRepository _mockUnitConversionData;
    private readonly string _massUnit1Id;
    private readonly string _massUnit2Id;
    private readonly string _volumeUnit1Id;

    public UnitConversionServiceTests()
    {
        var mockUnitConversionData = new Mock<IUnitConversionRepository>();
        var mockUnitData = new Mock<IUnitRepository>();

        Unit massUnit1 = new(testUserId, "Grams", UnitType.Mass);
        Unit volumeUnit1 = new(testUserId, "Cups", UnitType.Volume);
        Unit massUnit2 = new(testUserId, "Milligrams", UnitType.Mass);

        UnitConversion existingMassConversion
                        = new(testUserId, massUnit1.Id, massUnit2.Id, 1000)
        {
            UnitType = UnitType.Mass
        };

        _massUnit1Id = massUnit1.Id;
        _volumeUnit1Id = volumeUnit1.Id;
        _massUnit2Id = massUnit2.Id;

        Unit[] units = [massUnit1, volumeUnit1];

        foreach (Unit unit in units)
        {
            mockUnitData.Setup(_ => _.Get(testUserId, unit.Id)).ReturnsAsync(unit);
        }

        mockUnitConversionData.Setup(_ =>
            _.FindByUnitIdsEitherWay(testUserId, _massUnit1Id, _massUnit2Id)
        ).ReturnsAsync(existingMassConversion);

        _mockUnitData = mockUnitData.Object;
        _mockUnitConversionData = mockUnitConversionData.Object;
    }

    [Fact]
    public async void CreateUnitConversion_ThrowsIfUnitsAreOfDifferentTypes()
    {
        var mockUnitConvRepo = new Mock<IUnitConversionRepository>();

        UnitConversionService sut = new(_serviceProvider.Object, _mockUnitData,
                                                    _mockUnitConversionData);

        UnitConversionDto dto = new()
        {
            UnitId = _massUnit1Id,
            TargetUnitId = _volumeUnit1Id,
            TargetUnitsPerUnit = 2
        };

        await Assert.ThrowsAsync<ApplicationException>(
            async () => await sut.CreateUnitConversion(dto)); 
    }

    [Fact]
    public async void CreateUnitConversion_ThrowsIfUnitsAreTheSameUnit()
    {
        var mockUnitConvRepo = new Mock<IUnitConversionRepository>();

        UnitConversionService sut = new(_serviceProvider.Object, _mockUnitData,
                                                    _mockUnitConversionData);

        UnitConversionDto dto = new()
        {
            UnitId = _massUnit1Id,
            TargetUnitId = _massUnit1Id,
            TargetUnitsPerUnit = 2
        };

        await Assert.ThrowsAsync<ApplicationException>(
            async () => await sut.CreateUnitConversion(dto));   
    }

    [Fact]
    public async void CreateUnitConversion_ThrowsIfUnitsAlreadyHaveAConversion()
    {
        var mockUnitConvRepo = new Mock<IUnitConversionRepository>();

        UnitConversionService sut = new(_serviceProvider.Object, _mockUnitData,
                                                    _mockUnitConversionData);

        UnitConversionDto dto = new()
        {
            UnitId = _massUnit1Id,
            TargetUnitId = _massUnit2Id,
            TargetUnitsPerUnit = 2
        };

        await Assert.ThrowsAsync<ApplicationException>(
            async () => await sut.CreateUnitConversion(dto));   
    }
}
