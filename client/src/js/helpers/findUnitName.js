// This may be better if it were findUnit instead
export default function findUnitName(unitId, units)
{
    for (let i = 0; i < units.length; i += 1)
    {
        const unit = units[i];

        if (unit.id === unitId) {
            return unit.name;
        }
    }

    return "";
}
