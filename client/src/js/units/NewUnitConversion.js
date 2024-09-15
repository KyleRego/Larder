import { useContext } from "react";
import { AlertContext } from "../../AlertContext";
import UnitConversionsService from "../services/UnitConversionsService";
import UnitConversionForm from "./UnitConversionForm";

export default function NewUnitConversion({unit, setUnit, setNewingConversion}) {
    const { setAlertMessage } = useContext(AlertContext);

    const newUnitConversion = {
        unitId: unit.id
    };

    async function handleSubmit(e) {
        e.preventDefault();

        const formData = new FormData(e.target);
        const dto = {
            unitId: unit.id,
            targetUnitsPerUnit: formData.get("targetUnitsPerUnit"),
            targetUnitId: formData.get("targetUnitId")
        };

        const service = new UnitConversionsService();
        
        service.postUnitConversion(dto).then((result) => {
            const newUnit = structuredClone(unit);
            newUnit.conversions.push(result);
            setUnit(newUnit);
            setNewingConversion(false);

        }).catch(error => {
            setAlertMessage(`Something went wrong: ${error.message}`);
        });
    }

    return <div className="d-flex align-items-center column-gap-3">

        <UnitConversionForm unit={unit} unitConversion={newUnitConversion} handleSubmit={handleSubmit} />

        <button onClick={() => setNewingConversion(false)}
                type="button" className="btn btn-secondary btn-sm">
            Cancel
        </button>
    </div>
}
