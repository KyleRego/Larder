import { UnitDto } from "../../types/UnitDto";

export default function UnitCard({unit} : {unit: UnitDto}) {
    return <div className="card">
        <div className="card-header">
            {unit.name}
        </div>
        <div className="card-body">
            <p>Type: {UnitDto.getType(unit)}</p>
        </div>
    </div>
}