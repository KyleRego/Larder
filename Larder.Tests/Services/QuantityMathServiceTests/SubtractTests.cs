using Larder.Dtos;
using Larder.Models;
using Larder.Services.Impl;
using Larder.Tests.Mocks.Repository;

namespace Larder.Tests.Services.QuantityMathServiceTests;

public class SubtractTests : ServiceTestsBase
{
    [Fact]
    public async void SubtractUnitlessQuantitiesSubtractsTheAmount()
    {
        MockUnitData unitData = new();
        UnitService unitService = new(mSP.Object, unitData);
        UnitConversionService unitConversionService = new(mSP.Object,
                unitData, new MockUnitConversionData());

        QuantityDto minuend = new()
        {
           UnitId = null, Amount = 6 
        };

        QuantityDto subtrahend = new()
        {
            UnitId = null, Amount = 3.5
        };

        QuantityService sut = new(mSP.Object, unitService, unitConversionService);
        QuantityDto result = await sut.Subtract(minuend, subtrahend);

        Assert.Equal(2.5, result.Amount);
        Assert.Null(result.UnitId);
    }

    [Fact]
    public async void SubtractQuantityWithSameUnitSubtractsTheAmount()
    {
        MockUnitData unitData = new();
        UnitService unitService = new(mSP.Object, unitData);
        UnitConversionService unitConversionService = new(mSP.Object,
                unitData, new MockUnitConversionData());

        Unit unit = (await unitData.Get(mockUserId, "pounds"))!;

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

        QuantityService sut = new(mSP.Object, unitService, unitConversionService);

        QuantityDto result = await sut.Subtract(minuend, subtrahend);

        Assert.Equal(minuend.Amount - subtrahend.Amount, result.Amount);
        Assert.Equal(unit.Id, result.UnitId);
    }

    [Fact]
    public async void SubtractQuantityWithCompatibleUnitsDoesConversion()
    {
        MockUnitData unitData = new();
        UnitService unitService = new(mSP.Object, unitData);
        UnitConversionService unitConversionService = new(mSP.Object,
                unitData, new MockUnitConversionData());

        Unit grams = (await unitData.Get(mockUserId, "grams"))!;
        Unit milligrams = (await unitData.Get(mockUserId, "milligrams"))!;

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

        QuantityService sut = new(mSP.Object, unitService, unitConversionService);

        QuantityDto result = await sut.Subtract(minuend, subtrahend);

        Assert.Equal(1000, result.Amount);
        Assert.Equal(milligrams.Id, result.UnitId);
    } 
}
