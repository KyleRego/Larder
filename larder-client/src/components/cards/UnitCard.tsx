import { UnitDto } from "../../types/dtos/UnitDto";
import UnitName from "../UnitName";

export default function UnitCard({unit} : {unit: UnitDto}) {
    const conversions = unit.conversions.map(uc => {
        return <li className="list-group-item" key={uc.id}>
            1 <UnitName unitId={uc.unitId} /> = {uc.targetUnitsPerUnit} <UnitName unitId={uc.targetUnitId} />
        </li>;
    });

    return <div className="card">
        <div className="card-header">
            {unit.name}
        </div>
        <div className="card-body">
            <p>
                Type: {UnitDto.getType(unit)}
            </p>

            <h3 className="fs-3">
                Conversions
            </h3>

            <ul className="list-group list-group-flush">
                {conversions}
            </ul>
        </div>
    </div>
}