import { Link, useNavigate } from "react-router-dom";
import UnitForm from "../components/UnitForm";
import { UnitType } from "../types/UnitType";
import { apiClient } from "../util/axios";
import { Unit } from "../types/Unit";

export default function NewUnit() {
    const navigate = useNavigate();

    function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const formData = new FormData(e.currentTarget);
        const name = formData.get("name") as string;
        const unitType = parseInt(formData.get("type") as string) as UnitType;

        const newUnit = new Unit(null, name, unitType);

        apiClient.post<Unit>("/api/units", newUnit).then(_ => {
            navigate("/units");
        }).catch(error => {
            console.error(error);
        });
    }

    return (
        <>
            <div className="page-flex-header">
                <h1>New unit:</h1>

                <Link className="btn btn-danger" to={"/units"}>Cancel</Link>
            </div>

            <div className="mt-4">
                <UnitForm unit={null} handleSubmit={handleSubmit} />
            </div>
        </>
    );
}
