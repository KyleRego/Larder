import { Link, useNavigate } from "react-router-dom";
import UnitForm from "./UnitForm";
import UnitsService from "../services/UnitsService";
import UnitFormDataMapper from "./UnitFormDataMapper";
import { useContext } from "react";
import { AlertContext } from "../../AlertContext";
import { UnitsContext } from "../../UnitsContext";

export default function NewUnit() {
    const { units, setUnits } = useContext(UnitsContext);
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);

    async function handleSubmit(e) {
        e.preventDefault();
        const formData = new FormData(e.target);
        const dto = UnitFormDataMapper.map(formData);
        const service = new UnitsService();
        await service.postUnit(dto).then((result) => {
            const newUnits = structuredClone(units);
            newUnits.push(result);
            setUnits(newUnits);
            setAlertMessage(`Unit "${dto.name}" was created.`);
            navigate("/units");
        });
    }

    return <>
        <h1 className="mb-4">New unit:</h1>

        <UnitForm initialUnit={null} handleSubmit={handleSubmit} />

        <div className="mt-4">
            <Link to="/units">Back to units</Link> 
        </div>
    </>;
}
