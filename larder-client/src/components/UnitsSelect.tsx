import { useContext } from "react";
import { UnitsContext } from "../contexts/UnitsContext";

export default function UnitsSelect({selectName, selectTitle, defaultValue, required = false}
    : { selectName : string,
        selectTitle: string,
        defaultValue: string | null,
        required?: boolean }) {
    const { units } = useContext(UnitsContext);

    const options = units.map(u => {
        return <option key={u.id} value={u.id!}>
            {u.name}
        </option>
    });

    return <select defaultValue={defaultValue ?? ""}  id={selectName}
                    name={selectName}
                    title={selectTitle} className="form-select"
                    required = {required}>
        {options}
    </select>;
}