import { useState, useContext } from "react";
import { UnitsContext } from "../../UnitsContext";
import EditUnitConversion from "./EditUnitConversion";
import findUnitName from "../helpers/findUnitName";
import UnitConversionsService from "../services/UnitConversionsService";
import { AlertContext } from "../../AlertContext";

export default function UnitConversion({unit, setUnit, unitConversion}) {
    const { setAlertMessage } = useContext(AlertContext);
    const { units } = useContext(UnitsContext);
    const [editing, setEditing] = useState(false);

    const targetUnitName = findUnitName(unitConversion.targetUnitId, units);

    function handleDelete() {
        if (window.confirm("Are you sure you want to delete this conversion?")) {
            const service = new UnitConversionsService();

            service.deleteUnitConversion(unitConversion.id).then(() => {
                const newUnit = structuredClone(unit);
                for (let i = 0; i < newUnit.conversions.length; i += 1) {
                    if (newUnit.conversions[i].id === unitConversion.id) {
                        newUnit.conversions.splice(i, 1);
                        break;
                    }
                }
                setUnit(newUnit);
            }).catch(error => {
                setAlertMessage(`Something went wrong: ${error.message}`);
            })
        }
    }

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

        <button onClick={handleDelete} type="button" className="btn btn-danger btn-sm">
            Delete
        </button>
    </div>;
}