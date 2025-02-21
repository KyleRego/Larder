using Larder.Dtos;
using Larder.Models;

namespace Larder.Tests.Services.QuantityServiceTests;

public class ConvertQuantityTests : QuantityServiceTests
{
    [Fact]
    public async void TestConvertingGramsToMilligrams()
    {
        Unit grams = (await _unitData.Get(testUserId, "grams"))!;
        Unit milligrams = (await _unitData.Get(testUserId, "milligrams"))!;

        QuantityDto quantity = new()
        {
            Amount = 10,
            UnitId = grams.Id
        };

        QuantityDto result = await _sut.Convert(
            quantity, milligrams.Id);

        Assert.Equal(milligrams.Id, result.UnitId);
        Assert.Equal(10 * 1000, result.Amount);
    }

    [Fact]
    public async void TestConvertingMilligramsToGrams()
    {
        Unit grams = (await _unitData.Get(testUserId, "grams"))!;
        Unit milligrams = (await _unitData.Get(testUserId, "milligrams"))!;

        QuantityDto quantity = new()
        {
            Amount = 10000,
            UnitId = milligrams.Id
        };

        QuantityDto result = await _sut.Convert(
            quantity, grams.Id);

        Assert.Equal(grams.Id, result.UnitId);
        Assert.Equal(10000 / 1000, result.Amount);
    }
}
