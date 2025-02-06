using Larder.Dtos;
using Larder.Models;
using Larder.Services.Impl;
using Larder.Tests.Repository;

namespace Larder.Tests.Services.QuantityMathServiceTests;

public class ConvertQuantityTests : ServiceTestsBase
{
    private readonly Unit _gramsUnit;
    private readonly Unit _milligramsUnit;
    private readonly UnitConversionDto _gramsToMilligramsConversion;

    public ConvertQuantityTests()
    {
        MockUnitRepository unitData = new();
        MockUnitConversionRepository unitConversionData = new();

        _gramsUnit = unitData.Get(mockUserId, "grams").GetAwaiter().GetResult()!;
        _milligramsUnit = unitData.Get(mockUserId, "milligrams").GetAwaiter().GetResult()!;

        _gramsToMilligramsConversion = UnitConversionDto.FromEntity(
            unitConversionData.FindByUnitIdsEitherWay(
                mockUserId, _gramsUnit.Id, _milligramsUnit.Id).GetAwaiter().GetResult()!);
    }

    [Fact]
    public void TestConvertingGramsToMilligrams()
    {
        Quantity quantity = new()
        {
            Amount = 10,
            Unit = _gramsUnit,
            UnitId = _gramsUnit.Id
        };

        Quantity result = QuantityMathService.ConvertQuantity(
            quantity, _gramsToMilligramsConversion, _milligramsUnit);

        Assert.Equal(_milligramsUnit.Id, result.UnitId);
        Assert.Equal(10 * 1000, result.Amount);
    }

    [Fact]
    public void TestConvertingMilligramsToGrams()
    {
        Quantity quantity = new()
        {
            Amount = 10000,
            Unit = _milligramsUnit,
            UnitId = _milligramsUnit.Id
        };

        Quantity result = QuantityMathService.ConvertQuantity(
            quantity, _gramsToMilligramsConversion, _gramsUnit);

        Assert.Equal(_gramsUnit.Id, result.UnitId);
        Assert.Equal(10000 / 1000, result.Amount);
    }
}
