using Larder.Dtos;
using Larder.Models;
using Larder.Services.Impl;
using Larder.Tests.Mocks.Repository;

namespace Larder.Tests.Services.QuantityMathServiceTests;

public class SubtractUpToZeroTests : ServiceTestsBase
{
    [Fact]
    public async void FiveMinusSevenIsZero()
    {
        QuantityDto minuend = new() { Amount = 5, UnitId = null };
        QuantityDto subtrahend = new() { Amount = 7, UnitId = null };

        MockUnitData unitData = new();
        UnitService unitService = new(_serviceProvider.Object, unitData);
        UnitConversionService unitConversionService = new(_serviceProvider.Object,
                unitData, new MockUnitConversionData());
        QuantityService sut = new(_serviceProvider.Object, unitService, unitConversionService);

        QuantityDto result = await sut.SubtractUpToZero(minuend, subtrahend);
    
        Assert.Equal(0, result.Amount);
    }

    [Fact]
    public async void MinuendUnitIsKept()
    {
        MockUnitData unitData = new();
        Unit grams = (await unitData.Get(testUserId, "grams"))!;
        Unit milligrams = (await unitData.Get(testUserId, "milligrams"))!;

        QuantityDto minuend = new() { Amount = 300, UnitId = milligrams.Id };
        QuantityDto subtrahend = new() { Amount = 1, UnitId = grams.Id };
        
        UnitService unitService = new(_serviceProvider.Object, unitData);
        UnitConversionService unitConversionService = new(_serviceProvider.Object,
                unitData, new MockUnitConversionData());
        QuantityService sut = new(_serviceProvider.Object, unitService, unitConversionService);

        QuantityDto result = await sut.SubtractUpToZero(minuend, subtrahend);
    
        Assert.Equal(0, result.Amount);
        Assert.Equal(milligrams.Id, result.UnitId);
    }
}
