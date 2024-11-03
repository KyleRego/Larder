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
        string mockUserId = Guid.NewGuid().ToString();

        _gramsUnit = new(mockUserId, "Grams", UnitType.Mass);

        _milligramsUnit = new(mockUserId, "Milligrams", UnitType.Mass);

        _gramsToMilligramsConversion
                = new(mockUserId, _gramsUnit.Id, _milligramsUnit.Id, 1000)
        {
            UnitType = UnitType.Mass
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

        Quantity result = QuantityConverter.Convert(
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

        Quantity result = QuantityConverter.Convert(
            quantity, _gramsToMilligramsConversion, _gramsUnit);

        Assert.Equal(_gramsUnit.Id, result.UnitId);
        Assert.Equal(10000 / 1000, result.Amount);
    }
}
