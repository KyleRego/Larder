import { useNavigate } from "react-router-dom";
import { Unit } from "../types/Unit";

export default function UnitsTable({units} : {units: Unit[]}) {
    const unitRows = units.map(u => <UnitRow key={u.id} unit={u} />);

    return (
        <>
            <table className="table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th scope="col">Name</th>
                        <th scope="col">Type</th>
                    </tr>
                </thead>
                <tbody>
                    {unitRows}
                </tbody>
            </table>
        </>
    );
}

function UnitRow({unit} : {unit: Unit}) {
    const navigate = useNavigate();

    function handleRowClick() {
        navigate(`/units/${unit.id}`)
    }

    return (
        <tr role="button" onClick={handleRowClick}>
            <th scope="row">{unit.name}</th>
            <td>{Unit.getType(unit)}</td>
        </tr>
    );
}
