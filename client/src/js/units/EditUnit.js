import { useState, useEffect, useContext } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";

import UnitsService from "../services/UnitsService";
import UnitFormDataMapper from "./UnitFormDataMapper";
import UnitForm from "./UnitForm";
import { AlertContext } from "../../AlertContext";

export default function EditUnit()
{
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);
    const { id } = useParams();
    const [unit, setUnit] = useState(null);

    useEffect(() => {
        const service = new UnitsService();

        service.getUnit(id).then(result => {
            setUnit(result);
        })
    }, [id]);

    async function handleSubmit(e)
    {
        e.preventDefault();
        const formData = new FormData(e.target);
        const dto = UnitFormDataMapper.map(formData);
        dto.id = id;
        const service = new UnitsService();
        await service.putUnit(dto).then(() => {
            setAlertMessage(`Unit "${dto.name}" was created.`);
            navigate("/units");
        });
    }

    if (unit === null) return <h1>Loading...</h1>;

    return <>
        <h1>Editing unit</h1>

        <UnitForm initialUnit={unit} handleSubmit={handleSubmit} />

        <div>
            <Link to={`/units/${id}`}>Back to unit</Link>
        </div>
    </>
}