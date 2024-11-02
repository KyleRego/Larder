import { Unit } from "../types/Unit";
import { UnitConversion } from "../types/UnitConversion";
import UnitsSelect from "./UnitsSelect";

export default function UnitConversionForm({unit, handleSubmit, handleCancel, unitConversion}
    : { unit: Unit,
        handleSubmit: (e: React.FormEvent<HTMLFormElement>) => void,
        unitConversion: UnitConversion | null,
        handleCancel: () => void}) {

    const submitBtnText = unitConversion ? "Update conversion" : "Create conversion";

    return (
        <form onSubmit={handleSubmit}>
            <div className="d-flex flex-wrap column-gap-3 align-items-center row-gap-3">
                <div className="d-flex flex-wrap align-items-center column-gap-3 row-gap-1">
                    <div title="1 unit =">1 {unit.name} =</div>

                    <div>
                        <label htmlFor="targetUnitsPerUnit" hidden>Target units per unit:</label>
                        <input id="targetUnitsPerUnit" defaultValue={unitConversion?.targetUnitsPerUnit}
                                name="targetUnitsPerUnit"
                                title="Target units per unit" min="1" step="any" type="number" />
                    </div>

                    <div>
                        <UnitsSelect selectName="targetUnitId"
                                selectTitle="Choose the target unit"
                                defaultValue={unitConversion?.targetUnitId ?? null} />
                    </div>
                </div>

                <div>
                    <button type="submit" className="btn btn-sm btn-primary">{submitBtnText}</button>
                </div>

                <div>
                    <button onClick={handleCancel} type="button" className="btn btn-sm btn-danger">Cancel</button>
                </div>
            </div>  
        </form>
    );
}