using Larder.Models;
using Larder.Services;
using Larder.Services.Impl;

namespace Larder.Tests.Services.QuantityMathServiceTests;

public class SubtractTests : ServiceTestsBase
{
    private readonly Mock<IUnitConversionService> _unitConversionService = new();

    [Fact]
    public async void SubtractUnitlessQuantitiesSubtractsTheAmount()
    {
        Quantity minuend = new()
        {
           UnitId = null, Unit = null, Amount = 6 
        };
        Quantity subtrahend = new()
        {
            UnitId = null, Unit = null, Amount = 3.5
        };

        QuantityMathService sut = new(mSP.Object, _unitConversionService.Object);
        Quantity result = await sut.Subtract(minuend, subtrahend);

        Assert.Equal(2.5, result.Amount);
        Assert.Null(result.Unit);
    }

    [Fact]
    public async void SubtractQuantityWithSameUnitSubtractsTheAmount()
    {
        Unit unit = new(mockUserId, "Lbs", UnitType.Weight);

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

        QuantityMathService sut = new(mSP.Object, _unitConversionService.Object);

        Quantity result = await sut.Subtract(minuend, subtrahend);

        Assert.Equal(104-78, result.Amount);
        Assert.Equal(unit.Id, result.UnitId);
    }
}
