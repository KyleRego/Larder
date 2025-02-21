using Larder.Dtos;
using Larder.Models;

namespace Larder.Tests.Services.QuantityServiceTests;

public class SubtractTests : QuantityServiceTests
{
    [Fact]
    public async void SubtractUnitlessQuantitiesSubtractsTheAmount()
    {
        QuantityDto minuend = new()
        {
           UnitId = null, Amount = 6 
        };

        QuantityDto subtrahend = new()
        {
            UnitId = null, Amount = 3.5
        };

        QuantityDto result = await _sut.Subtract(minuend, subtrahend);

        Assert.Equal(2.5, result.Amount);
        Assert.Null(result.UnitId);
    }

    [Fact]
    public async void SubtractQuantityWithSameUnitSubtractsTheAmount()
    {
        Unit unit = (await _unitData.Get(testUserId, "pounds"))!;

        QuantityDto minuend = new()
        {
            Amount = 104,
            UnitId = unit.Id
        };

        QuantityDto subtrahend = new()
        {
            Amount = 78,
            UnitId = unit.Id
        };

        QuantityDto result = await _sut.Subtract(minuend, subtrahend);

        Assert.Equal(minuend.Amount - subtrahend.Amount, result.Amount);
        Assert.Equal(unit.Id, result.UnitId);
    }

    [Fact]
    public async void SubtractQuantityWithCompatibleUnitsDoesConversion()
    {
        Unit grams = (await _unitData.Get(testUserId, "grams"))!;
        Unit milligrams = (await _unitData.Get(testUserId, "milligrams"))!;

        QuantityDto minuend = new()
        {
            Amount = 2000,
            UnitId = milligrams.Id
        };

        QuantityDto subtrahend = new()
        {
            Amount = 1,
            UnitId = grams.Id
        };

        QuantityDto result = await _sut.Subtract(minuend, subtrahend);

        Assert.Equal(1000, result.Amount);
        Assert.Equal(milligrams.Id, result.UnitId);
    } 
}
