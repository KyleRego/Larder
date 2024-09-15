import { useState, useContext } from "react";
import { UnitsContext } from "../../UnitsContext";
import EditUnitConversion from "./EditUnitConversion";
import findUnitName from "../helpers/findUnitName";

export default function UnitConversion({unit, setUnit, unitConversion}) {
    const { units } = useContext(UnitsContext);
    const [editing, setEditing] = useState(false);

    const targetUnitName = findUnitName(unitConversion.targetUnitId, units);

    if (editing === true) {
        return <EditUnitConversion unit={unit} setUnit={setUnit} unitConversion={unitConversion} setEditing={setEditing} />
    }

    return <div className="d-flex align-items-center column-gap-3">
        <span>
            1 {unit.name} = {unitConversion.targetUnitsPerUnit} {targetUnitName}
        </span>

        <button onClick={() => setEditing(true)} type="button" className="btn btn-secondary btn-sm">
            Edit
        </button>
    </div>;
}