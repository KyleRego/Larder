import { useState, useEffect, useContext } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import UnitsService from "../services/UnitsService";
import UnitFormDataMapper from "./UnitFormDataMapper";
import UnitForm from "./UnitForm";
import { AlertContext } from "../../AlertContext";
import { UnitsContext } from "../../UnitsContext";

export default function EditUnit() {
    const { units, setUnits } = useContext(UnitsContext);
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);
    const { id } = useParams();
    const [unit, setUnit] = useState(null);

    useEffect(() => {
        const service = new UnitsService();

        service.getUnit(id).then(res => {
            setUnit(res);
        })
    }, [id]);

    async function handleSubmit(e) {
        e.preventDefault();
        const formData = new FormData(e.target);
        const dto = UnitFormDataMapper.map(formData);
        dto.id = id;
        const service = new UnitsService();
        await service.putUnit(dto).then((result) => {
            const newUnits = structuredClone(units);
            for (let i = 0; i < newUnits.length; i += 1) {
                if (newUnits[i].id === result.id) {
                    newUnits[i] = result;
                }
            }
            setUnits(newUnits);
            setAlertMessage(`Unit "${dto.name}" was created.`);
            navigate("/units");
        });
    }

    if (unit === null) return <h1>Loading...</h1>;

    return <>
        <h1 className="mb-4">Editing unit: {unit.name}</h1>

        <UnitForm initialUnit={unit} handleSubmit={handleSubmit} />

        <div className="mt-4">
            <Link to={`/units/${id}`}>Back to unit</Link>
        </div>
    </>;
}
