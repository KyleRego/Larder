import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";

import UnitsService from "../services/UnitsService";
import UnitFormDataMapper from "./UnitFormDataMapper";
import UnitForm from "./UnitForm";

export default function EditUnit()
{
    const { id } = useParams();
    const [unit, setUnit] = useState(null);

    useEffect(() => {
        const service = new UnitsService();

        service.getUnit(id).then(result => {
            setUnit(result);
        })
    }, [id]);

    if (unit === null) return <h1>Loading...</h1>;

    async function handleSubmit(e)
    {
        e.preventDefault();
        const formData = new FormData(e.target);
        const dto = UnitFormDataMapper.map(formData);
        dto.id = id;
        const service = new UnitsService();
        await service.putUnit(dto);
    }

    return <>
        <h1>Editing unit</h1>

        <UnitForm initialUnit={unit} handleSubmit={handleSubmit} />

        <div>
            <Link to={`/units/${id}`}>Back to unit</Link>
        </div>
    </>
}