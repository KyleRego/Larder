import { useContext } from "react";
import { UnitsContext } from "../../UnitsContext";
import LabeledInput from "../components/LabeledInput";

export default function UnitConversionForm({unit, unitConversion, handleSubmit})
{
    const { units } = useContext(UnitsContext)

    const targetUnitOptions = units.map(tu => {
        return <option key={tu.id} value={tu.id}>
            {tu.name}
        </option>
    });

    return <form onSubmit={(e) => handleSubmit(e)}>
        <div className="d-flex column-gap-1 flex-wrap align-items-center">

            <span>1 {unit.name} =</span>

            <div>
                <LabeledInput inputName="targetUnitsPerUnit"
                                labelText=""
                                initialValue={unitConversion.targetUnitsPerUnit}
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

            <button type="submit" className="btn btn-primary btn-sm">
                Submit
            </button>
        </div>
    </form>
}
