using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Larder.Services;

namespace Larder.Tests.Services;

public class UnitConversionServiceTests : ServiceTestsBase
{
    private readonly Dictionary<string, Unit> _unitMap;

    public UnitConversionServiceTests()
    {
        _unitMap = [];
        _unitMap["1"] = new() { UserId = mockUserId, Id = "1", Type = UnitType.Mass, Name = "Grams" };
        _unitMap["2"] = new() { UserId = mockUserId, Id = "2", Type = UnitType.Volume, Name = "Cups" };
    }

    [Fact]
    public async void CreateUnitConversionThrowsIfUnitsAreOfDifferentTypes()
    {
        var mockUnitRepo = new Mock<IUnitRepository>();
        string unitId = "1";
        Unit unit = _unitMap[unitId];
        string targetUnitId = "2";
        Unit targetUnit = _unitMap[targetUnitId];

        mockUnitRepo.Setup(_ => _.Get(mockUserId, unitId)).ReturnsAsync(unit);
        mockUnitRepo.Setup(_ => _.Get(mockUserId, targetUnitId)).ReturnsAsync(targetUnit);

        var mockUnitConvRepo = new Mock<IUnitConversionRepository>();

        UnitConversionService sut = new(mSP.Object, mockUnitRepo.Object,
                                                    mockUnitConvRepo.Object);

        UnitConversionDto dto = new()
        {
            UnitId = unitId,
            TargetUnitId = targetUnitId,
            TargetUnitsPerUnit = 2
        };

        await Assert.ThrowsAsync<ApplicationException>(async () => await sut.CreateUnitConversion(dto)); 
    }
}
