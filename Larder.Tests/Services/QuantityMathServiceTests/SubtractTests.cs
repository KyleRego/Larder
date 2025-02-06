using Larder.Models;
using Larder.Services.Impl;
using Larder.Tests.Repository;

namespace Larder.Tests.Services.QuantityMathServiceTests;

public class SubtractTests : ServiceTestsBase
{
    [Fact]
    public async void SubtractUnitlessQuantitiesSubtractsTheAmount()
    {
        MockUnitRepository unitData = new();
        UnitService unitService = new(mSP.Object, unitData);
        UnitConversionService unitConversionService = new(mSP.Object,
                unitData, new MockUnitConversionRepository());

        Quantity minuend = new()
        {
           UnitId = null, Unit = null, Amount = 6 
        };

        Quantity subtrahend = new()
        {
            UnitId = null, Unit = null, Amount = 3.5
        };

        QuantityMathService sut = new(mSP.Object, unitService, unitConversionService);
        Quantity result = await sut.Subtract(minuend, subtrahend);

        Assert.Equal(2.5, result.Amount);
        Assert.Null(result.Unit);
    }

    [Fact]
    public async void SubtractQuantityWithSameUnitSubtractsTheAmount()
    {
        MockUnitRepository unitData = new();
        UnitService unitService = new(mSP.Object, unitData);
        UnitConversionService unitConversionService = new(mSP.Object,
                unitData, new MockUnitConversionRepository());

        Unit unit = (await unitData.Get(mockUserId, "pounds"))!;

        Quantity minuend = new()
        {
            Amount = 104,
            Unit = unit,
            UnitId = unit.Id
        };

        Quantity subtrahend = new()
        {
            Amount = 78,
            Unit = unit,
            UnitId = unit.Id
        };

        QuantityMathService sut = new(mSP.Object, unitService, unitConversionService);

        Quantity result = await sut.Subtract(minuend, subtrahend);

        Assert.Equal(minuend.Amount - subtrahend.Amount, result.Amount);
        Assert.Equal(unit.Id, result.UnitId);
    }

    [Fact]
    public async void SubtractQuantityWithCompatibleUnitsDoesConversion()
    {
        MockUnitRepository unitData = new();
        UnitService unitService = new(mSP.Object, unitData);
        UnitConversionService unitConversionService = new(mSP.Object,
                unitData, new MockUnitConversionRepository());

        Unit grams = (await unitData.Get(mockUserId, "grams"))!;
        Unit milligrams = (await unitData.Get(mockUserId, "milligrams"))!;

        Quantity minuend = new()
        {
            Amount = 2000,
            UnitId = milligrams.Id,
            Unit = milligrams
        };

        Quantity subtrahend = new()
        {
            Amount = 1,
            UnitId = grams.Id,
            Unit = grams
        };

        QuantityMathService sut = new(mSP.Object, unitService, unitConversionService);

        Quantity result = await sut.Subtract(minuend, subtrahend);

        Assert.Equal(1000, result.Amount);
        Assert.Equal(milligrams.Id, result.UnitId);
    } 
}
