import { useContext } from "react";
import { UnitsContext } from "../contexts/UnitsContext";

export default function UnitsSelect({selectName, selectTitle, value, required = false, onChange = () => {}}
    : { selectName : string,
        selectTitle: string,
        value: string | null,
        required?: boolean,
        onChange?: (e: React.ChangeEvent<HTMLSelectElement>) => void }) {
    const { units } = useContext(UnitsContext);

    const options = units.map(u => {
        return <option key={u.id} value={u.id!}>
            {u.name}
        </option>
    });

    return <select id={selectName} value={value ?? ""} style={{maxWidth: "7rem"}} 
                    name={selectName}
                    title={selectTitle} className="form-select"
                    required = {required} onChange={onChange}>
        <option value="">
            No unit
        </option>
        {options}
    </select>;
}