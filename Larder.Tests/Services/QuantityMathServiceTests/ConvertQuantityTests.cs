using Larder.Dtos;
using Larder.Models;
using Larder.Services.Impl;
using Larder.Tests.Mocks.Repository;

namespace Larder.Tests.Services.QuantityMathServiceTests;

public class ConvertQuantityTests : ServiceTestsBase
{
    [Fact]
    public async void TestConvertingGramsToMilligrams()
    {
        MockUnitData unitData = new();
        UnitService unitService = new(mSP.Object, unitData);
        UnitConversionService unitConversionService = new(mSP.Object,
                unitData, new MockUnitConversionData());

        Unit grams = (await unitData.Get(mockUserId, "grams"))!;
        Unit milligrams = (await unitData.Get(mockUserId, "milligrams"))!;

        QuantityDto quantity = new()
        {
            Amount = 10,
            UnitId = grams.Id
        };

        QuantityService sut = new(mSP.Object, unitService, unitConversionService);

        QuantityDto result = await sut.Convert(
            quantity, milligrams.Id);

        Assert.Equal(milligrams.Id, result.UnitId);
        Assert.Equal(10 * 1000, result.Amount);
    }

    [Fact]
    public async void TestConvertingMilligramsToGrams()
    {
        MockUnitData unitData = new();
        UnitService unitService = new(mSP.Object, unitData);
        UnitConversionService unitConversionService = new(mSP.Object,
                unitData, new MockUnitConversionData());

        Unit grams = (await unitData.Get(mockUserId, "grams"))!;
        Unit milligrams = (await unitData.Get(mockUserId, "milligrams"))!;

        QuantityDto quantity = new()
        {
            Amount = 10000,
            UnitId = milligrams.Id
        };

        QuantityService sut = new(mSP.Object, unitService, unitConversionService);

        QuantityDto result = await sut.Convert(
            quantity, grams.Id);

        Assert.Equal(grams.Id, result.UnitId);
        Assert.Equal(10000 / 1000, result.Amount);
    }
}
