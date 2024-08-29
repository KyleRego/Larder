using Larder.Helpers;
using Larder.Models;

namespace Larder.Tests.Helpers;

public class QuantityConverterTests
{
    private readonly Unit _gramsUnit;
    private readonly Unit _milligramsUnit;
    private readonly UnitConversion _gramsToMilligramsConversion;

    public QuantityConverterTests()
    {
        _gramsUnit = new()
        {
            Type = UnitType.Mass,
            Name = "Grams"
        };

        _milligramsUnit = new()
        {
            Type = UnitType.Mass,
            Name = "Milligrams"
        };

        _gramsToMilligramsConversion = new()
        {
            UnitId = _gramsUnit.Id,
            Unit = _gramsUnit,
            TargetUnitId = _milligramsUnit.Id,
            TargetUnit = _milligramsUnit,
            TargetUnitsPerUnit = 1000
        };
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

        Quantity result = QuantityConverter.Convert(quantity, _gramsToMilligramsConversion, _milligramsUnit);
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

        Quantity result = QuantityConverter.Convert(quantity, _gramsToMilligramsConversion, _gramsUnit);
        Assert.Equal(_gramsUnit.Id, result.UnitId);
        Assert.Equal(10000 / 1000, result.Amount);
    }
}
