import { useContext } from "react";
import { UnitsContext } from "../contexts/UnitsContext";

// TODO: It would be good to store a dictionary of unitId -> unitName
// and not have to repeat finding the unit name by ID
export default function UnitName({unitId}: {unitId: string}) {
    const { units } = useContext(UnitsContext);
    let unitName: string | null = null;

    for (const u of units) {
        if (u.id === unitId) {
            unitName = u.name
        }
    }

    return <span>
        {unitName === null ? "" : unitName}
    </span>
}