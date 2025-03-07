import { UnitDto } from "../../types/dtos/UnitDto";
import { UnitType } from "../../types/dtos/UnitType";

export default function UnitForm({unit, handleSubmit}: {
                unit: UnitDto | null,
                handleSubmit: (e: React.FormEvent<HTMLFormElement>) => void
    }) {
    
    return (
        <form id="unit-form" onSubmit={handleSubmit}>
            <div>
                <label className="form-label" htmlFor="name">Unit name:</label>
                <input required className="form-control" 
                        id="name" name="name" type="text"
                        defaultValue={unit?.name} />
            </div>

            <div className="mt-4">
                <label className="form-label" htmlFor="type">Unit type:</label>
                <select defaultValue={unit?.type} className="form-select" id="type" name="type">
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
        </form>
    );
}
