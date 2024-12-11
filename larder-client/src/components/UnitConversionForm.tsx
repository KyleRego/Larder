import { useState } from "react";
import { UnitConversionDto } from "../types/UnitConversionDto";
import UnitsSelect from "./UnitsSelect";

export default function UnitConversionForm({handleSubmit, handleCancel, initialUnitConversion}
    : { handleSubmit: (e: React.FormEvent<HTMLFormElement>) => void,
        initialUnitConversion: UnitConversionDto | null,
        handleCancel: () => void}) {
    const [unitConversion, setUnitConversion] = useState<UnitConversionDto>(
        initialUnitConversion ?? { id: null, unitId: "", targetUnitId: "", targetUnitsPerUnit: 1}
    )
    const submitBtnText = unitConversion ? "Update conversion" : "Create conversion";

    function updateUnitConversion<K extends keyof UnitConversionDto>(field: K, value: UnitConversionDto[K]) {
        setUnitConversion(
            { ...unitConversion, [field]: value}
        );
    }

    return (
        <div className="shadow-sm p-4 border">
            <form onSubmit={handleSubmit}>
                <div className="d-flex flex-column flex-wrap column-gap-5 align-items-center row-gap-3">
                    <div className="d-flex flex-wrap align-items-center column-gap-2 row-gap-1">
                        <div className="d-flex column-gap-2 align-items-center">
                            <span>1</span>
                            <UnitsSelect selectName="unitId"
                                    selectTitle="Choose unit"
                                    value={unitConversion?.unitId ?? null}
                                    onChange={(e) => updateUnitConversion("unitId", e.target.value)} />
                            <span>=</span>
                        </div>

                        <div>
                            <label htmlFor="targetUnitsPerUnit" hidden>Target units per unit:</label>
                            <input id="targetUnitsPerUnit" defaultValue={unitConversion?.targetUnitsPerUnit}
                                    name="targetUnitsPerUnit"
                                    title="Target units per unit" min="1" step="any" type="number"
                                    className="form-control" required
                                    value={unitConversion.targetUnitsPerUnit}
                                    onChange={(e) => updateUnitConversion("targetUnitsPerUnit", parseFloat(e.target.value))} />
                        </div>

                        <div>
                            <UnitsSelect selectName="targetUnitId"
                                    selectTitle="Choose target unit"
                                    value={unitConversion?.targetUnitId ?? null}
                                    onChange={(e) => updateUnitConversion("targetUnitId", e.target.value)} />
                        </div>
                    </div>

                    <div className="d-flex flex-wrap column-gap-3 align-items-center row-gap-3">
                        <div>
                            <button type="submit" className="btn btn-sm btn-primary">{submitBtnText}</button>
                        </div>

                        <div>
                            <button onClick={handleCancel} type="button" className="btn btn-sm btn-danger">Cancel</button>
                        </div>
                    </div>
                </div>  
            </form>
        </div>
    );
}