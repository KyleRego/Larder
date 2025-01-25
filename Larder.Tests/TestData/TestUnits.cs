using Larder.Dtos;
using Larder.Models;

namespace Larder.Tests.TestData;

public static class TestUnits
{
    public static Unit Cups()
    {
        return new(TestUser.TestUserId(), "Cups", UnitType.Volume);
    }

    public static UnitDto CupsDto()
    {
        return UnitDto.FromEntity(Cups());
    }

    public static Unit TableSpoons()
    {
        return new(TestUser.TestUserId(), "Tablespoons", UnitType.Volume);
    }

    public static UnitDto TableSpoonsDto()
    {
        return UnitDto.FromEntity(TableSpoons());
    }
}
