import { useState } from "react";
import { UnitDto } from "../../types/dtos/UnitDto";
import { UnitType } from "../../types/dtos/UnitType";

export default function UnitForm({initialUnit, handleSubmit}: {
                initialUnit: UnitDto | null,
                handleSubmit: (unit: UnitDto) => void
    }) {

    const [unit, setUnit] = useState<UnitDto>(
        initialUnit || new UnitDto(null, "", UnitType.Mass, []));

    function handleFormSubmit(e: React.FormEvent) {
        e.preventDefault();
        handleSubmit(unit);
    }

    return (
        <form id="unit-form" onSubmit={handleFormSubmit}>
            <div>
                <label className="form-label" htmlFor="name">
                    Name:
                </label>
                <input required className="form-control" id="name" name="name" type="text" value={unit.name} onChange={(e) => setUnit({...unit, name : e.target.value})} />
            </div>

            <div className="mt-2">
                <label className="form-label" htmlFor="type">
                    Type:
                </label>
                <select value={unit.type} onChange={(e) => setUnit({...unit, type: parseInt(e.target.value) as UnitType})} className="form-select" id="type" name="type">
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
