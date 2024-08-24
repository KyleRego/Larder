import { Link } from "react-router-dom";

import UnitForm from "./UnitForm";

import UnitsService from "../services/UnitsService";
import UnitFormDataMapper from "./UnitFormDataMapper";

export default function NewUnit()
{
    async function handleSubmit(e)
    {
        e.preventDefault();
        const formData = new FormData(e.target);
        const dto = UnitFormDataMapper.map(formData);
        const service = new UnitsService();
        await service.postUnit(dto);
    }

    return <>
        <h1>New unit:</h1>

        <UnitForm initialUnit={null} handleSubmit={handleSubmit} />

        <Link to="/units">Back to units</Link> 
    </>
}