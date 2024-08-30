export default class UnitHelpers
{
    static UnitTypeEnumValueToText(enumValue)
    {
        switch (enumValue)
        {
            case 0:
                return "Mass";
            case 1:
                return "Volume";
            case 2:
                return "Weight";

            default:
                break;
        }
    }
}
