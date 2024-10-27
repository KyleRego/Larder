import { Unit } from "../types/Unit";
import { UnitType } from "../types/UnitType";

export default function UnitForm({unit, handleSubmit}: {
                unit: Unit | null,
                handleSubmit: (e: React.FormEvent<HTMLFormElement>) => void
    }) {

    const submitText = unit === null ? "Create unit" : "Update unit";
    
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label htmlFor="name">Unit name:</label>
                <input required className="form-control" 
                        id="name" name="name" type="text"
                        value={unit?.name} />
            </div>

            <div className="mt-4">
                <label htmlFor="type">Unit type:</label>
                <select className="form-select" id="type" name="type">
                    <option value={UnitType.Mass}>
                        Mass
                    </option>
                    <option value={UnitType.Volume}>
                        Volume
                    </option>
                    <option value={UnitType.Weight}>
                        Weight
                    </option>
                </select>
            </div>

            <div className="mt-4">
                <button className="btn btn-primary" type="submit">{submitText}</button>
            </div>
        </form>
    );
}
