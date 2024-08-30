import { useState, useEffect } from "react";

import LabeledInput from "../components/LabeledInput";
import UnitsService from "../services/UnitsService";

export default function UnitConversionForm({unit})
{
    const [targetUnits, setTargetUnits] = useState([]);

    useEffect(() => {
        const service = new UnitsService();
    
        service.getUnits().then(result => {
            setTargetUnits(result);
        });
    }, []);

    const targetUnitOptions = targetUnits.map(tu => {
        return <option key={tu.id} value={tu.id}>
            {tu.name}
        </option>
    });

    return <form>
        <div className="d-flex column-gap-1 flex-wrap align-items-center">

            <span>1 {unit.name} =</span>

            <div>
                <LabeledInput inputName="targetUnitsPerUnit"
                                labelText=""
                                initialValue={1}
                                afterInputText=""
                                inputType="number"
                                required={true} />
            </div>

            <div>
                <label hidden htmlFor="targetUnitId">Target unit:</label>
                <select name="targetUnitId">
                    {targetUnitOptions}
                </select>
            </div>
        </div>
    </form>
}
