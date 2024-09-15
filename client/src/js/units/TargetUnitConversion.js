import { useContext } from "react";
import { Link } from "react-router-dom";
import { UnitsContext } from "../../UnitsContext";
import findUnitName from "../helpers/findUnitName";

export default function TargetUnitConversion({targetUnit, unitConversion}) {
    const { units } = useContext(UnitsContext);

    const unitName = findUnitName(unitConversion.unitId, units)

    return <div className="d-flex align-items-center column-gap-3"
                title="Edit this conversion from the other unit">
        <span>
            1 <Link to={`/units/${unitConversion.unitId}`}>{unitName}</Link> = {unitConversion.targetUnitsPerUnit} {targetUnit.name}
        </span>

        <button disabled type="button" className="btn btn-secondary btn-sm">
            Edit
        </button>
    </div>;
}