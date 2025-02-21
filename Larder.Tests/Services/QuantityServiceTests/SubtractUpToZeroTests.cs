using Larder.Dtos;
using Larder.Models;

namespace Larder.Tests.Services.QuantityServiceTests;

public class SubtractUpToZeroTests : QuantityServiceTests
{
    [Fact]
    public async void FiveMinusSevenIsZero()
    {
        QuantityDto minuend = new() { Amount = 5, UnitId = null };
        QuantityDto subtrahend = new() { Amount = 7, UnitId = null };

        QuantityDto result = await _sut.SubtractUpToZero(minuend, subtrahend);
    
        Assert.Equal(0, result.Amount);
    }

    [Fact]
    public async void MinuendUnitIsKept()
    {
        Unit grams = (await _unitData.Get(testUserId, "grams"))!;
        Unit milligrams = (await _unitData.Get(testUserId, "milligrams"))!;

        QuantityDto minuend = new() { Amount = 300, UnitId = milligrams.Id };
        QuantityDto subtrahend = new() { Amount = 1, UnitId = grams.Id };

        QuantityDto result = await _sut.SubtractUpToZero(minuend, subtrahend);
    
        Assert.Equal(0, result.Amount);
        Assert.Equal(milligrams.Id, result.UnitId);
    }
}
