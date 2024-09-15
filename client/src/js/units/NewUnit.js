import { Link, useNavigate } from "react-router-dom";

import UnitForm from "./UnitForm";

import UnitsService from "../services/UnitsService";
import UnitFormDataMapper from "./UnitFormDataMapper";
import { useContext } from "react";
import { AlertContext } from "../../AlertContext";

export default function NewUnit()
{
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);

    async function handleSubmit(e)
    {
        e.preventDefault();
        const formData = new FormData(e.target);
        const dto = UnitFormDataMapper.map(formData);
        const service = new UnitsService();
        await service.postUnit(dto).then(() => {
            setAlertMessage(`Unit "${dto.name}" was created.`);
            navigate("/units");
        });
    }

    return <>
        <h1>New unit:</h1>

        <UnitForm initialUnit={null} handleSubmit={handleSubmit} />

        <Link to="/units">Back to units</Link> 
    </>
}