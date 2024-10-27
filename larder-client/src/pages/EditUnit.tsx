import { useEffect, useState } from "react";
import { Unit } from "../types/Unit";
import { useNavigate, useParams } from "react-router";
import { apiClient } from "../util/axios";
import Loading from "../components/Loading";
import { UnitType } from "../types/UnitType";
import { Link } from "react-router-dom";
import UnitForm from "../components/UnitForm";

export default function EditUnit() {
    const [unit, setUnit] = useState<Unit | null>(null);
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();

    useEffect(() => {
        apiClient.get<Unit>(`/api/units/${id}`).then(res => {
            setUnit(res.data);
        }).catch(error => {
            console.error(error);
        })
    }, []);

    function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const formData = new FormData(e.currentTarget);
        const name = formData.get("name") as string;
        const unitType = parseInt(formData.get("type") as string) as UnitType;

        const newUnit = new Unit(null, name, unitType);

        apiClient.put<Unit>("/api/units", newUnit).then(res => {
            navigate(`/units/${res.data.id}`);
        }).catch(error => {
            console.error(error);
        });
    }

    if (unit === null) return <Loading />;

    return (
        <>
            <div className="page-flex-header">
                <h1>Editing unit: {unit.name}</h1>

                <Link className="btn btn-danger" to={`/units/${unit.id}`}>Cancel</Link>
            </div>

            <div className="mt-4">
                <UnitForm unit={unit} handleSubmit={handleSubmit} />
            </div>
        </>
    );
}
