using Larder.Dtos;
using Larder.Models;
using Larder.Services.Impl;

namespace Larder.Tests.Services.QuantityMathServiceTests;

// This is testing a static method so there is no need
// to inherit ServiceTestsBase
public class ConvertQuantityTests
{
    private readonly Unit _gramsUnit;
    private readonly Unit _milligramsUnit;
    private readonly UnitConversionDto _gramsToMilligramsConversion;

    public ConvertQuantityTests()
    {
        string mockUserId = Guid.NewGuid().ToString();

        _gramsUnit = new(mockUserId, "Grams", UnitType.Mass);

        _milligramsUnit = new(mockUserId, "Milligrams", UnitType.Mass);

        _gramsToMilligramsConversion
                = UnitConversionDto.FromEntity(
                    new(mockUserId, _gramsUnit.Id, _milligramsUnit.Id, 1000)
            {
                UnitType = UnitType.Mass
            }
        );
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
