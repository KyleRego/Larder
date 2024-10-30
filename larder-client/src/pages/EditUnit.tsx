import { useContext, useEffect, useState } from "react";
import { Unit } from "../types/Unit";
import { useNavigate, useParams } from "react-router";
import { apiClient } from "../util/axios";
import Loading from "../components/Loading";
import { UnitType } from "../types/UnitType";
import { Link } from "react-router-dom";
import UnitForm from "../components/UnitForm";
import { MessageContext } from "../contexts/MessageContext";
import { ApiResponse } from "../types/ApiResponse";
import DeleteBtn from "../components/DeleteBtn";

export default function EditUnit() {
    const [unit, setUnit] = useState<Unit | null>(null);
    const { id } = useParams<{ id: string }>();
    const unitId = id as string;
    const navigate = useNavigate();
    const { setMessage } = useContext(MessageContext);

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

        const editedUnit = new Unit(unitId, name, unitType);

        apiClient.put<ApiResponse<Unit>>(`/api/units/${unitId}`, editedUnit).then(res => {
            setMessage({text: res.data.message, type: res.data.type})
            navigate(`/units/${res.data.data.id}`);
        }).catch(error => {
            console.error(error);
        });
    }

    async function handleDelete() {
        if (window.confirm(`Are you sure you want to delete unit ${unit?.name}?`)) {
            const response = await apiClient.delete(`api/units/${unitId}`);
            setMessage({text: response.data.message, type: response.data.type});
            navigate("/units");
        }
    }

    if (unit === null) return <Loading />;

    return (
        <>
            <div className="page-flex-header">
                <div className="d-flex column-gap-5 align-items-center">
                    <h1>Editing unit: {unit.name}</h1>

                    <Link className="btn btn-danger" to={`/units/${unit.id}`}>Cancel</Link>
                </div>

                <DeleteBtn handleClick={handleDelete} />
            </div>

            <div className="mt-4">
                <UnitForm unit={unit} handleSubmit={handleSubmit} />
            </div>
        </>
    );
}
