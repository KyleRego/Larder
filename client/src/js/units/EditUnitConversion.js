import { useContext } from "react";
import { AlertContext } from "../../AlertContext";
import UnitConversionsService from "../services/UnitConversionsService";
import UnitConversionForm from "./UnitConversionForm";

export default function NewUnitConversion({unit, setUnit, unitConversion, setEditing}) {
    const { setAlertMessage } = useContext(AlertContext);

    async function handleSubmit(e) {
        e.preventDefault();

        const formData = new FormData(e.target);
        const dto = {
            id: unitConversion.id,
            unitId: unit.id,
            targetUnitsPerUnit: formData.get("targetUnitsPerUnit"),
            targetUnitId: formData.get("targetUnitId")
        };

        const service = new UnitConversionsService();
        
        service.putUnitConversion(dto).then((result) => {
            const newUnit = structuredClone(unit);
            for (let i = 0; i < newUnit.conversions.length; i += 1) {
                if (newUnit.conversions[i].id === result.id) {
                    newUnit.conversions[i] = result;
                    break;
                }
            }
            setUnit(newUnit);
            setEditing(false);
        }).catch(error => {
            setAlertMessage(`Something went wrong: ${error.message}`);
        });
    }

    return <div className="d-flex align-items-center column-gap-3">

        <UnitConversionForm unit={unit} unitConversion={unitConversion} handleSubmit={handleSubmit} />

        <button onClick={() => setEditing(false)}
                type="button" className="btn btn-secondary btn-sm">
            Cancel
        </button>
    </div>
}
