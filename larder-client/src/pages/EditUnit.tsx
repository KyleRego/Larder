import { useContext, useEffect, useState } from "react";
import { UnitDto } from "../types/UnitDto";
import { useNavigate, useParams } from "react-router";
import { apiClient } from "../util/axios";
import Loading from "../components/Loading";
import { UnitType } from "../types/UnitType";
import UnitForm from "../components/UnitForm";
import { MessageContext } from "../contexts/MessageContext";
import { ApiResponse } from "../types/ApiResponse";
import ActionBar from "../ActionBar";
import BreadCrumbs from "../Breadcrumbs";
import { Link } from "react-router-dom";

export default function EditUnit() {
    const [unit, setUnit] = useState<UnitDto | null>(null);
    const { id } = useParams<{ id: string }>();
    const unitId = id as string;
    const navigate = useNavigate();
    const { setMessage } = useContext(MessageContext);

    useEffect(() => {
        apiClient.get<UnitDto>(`/api/units/${id}`).then(res => {
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

        const editedUnit = new UnitDto(unitId, name, unitType, []);

        apiClient.put<ApiResponse<UnitDto>>(`/api/units/${unitId}`, editedUnit).then(res => {
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
        <div className="d-flex flex-column h-100">
            <BreadCrumbs>
                <li className="breadcrumb-item">
                    <Link to={"/units"}>Units</Link>
                </li>
                <li className="breadcrumb-item">
                    <Link to={`/units/${unit.id}`}>{unit.name}</Link>
                </li>
                <li className="breadcrumb-item active">
                    Editing unit
                </li>
            </BreadCrumbs>

            <div className="container flex-grow-1">
                <UnitForm unit={unit} handleSubmit={handleSubmit} />
            </div>

            <ActionBar>
                <div className="d-flex justify-content-between">
                    <span className="invisible btn">Delete unit</span>
                    <button type="submit" form="unit-form"
                        className="btn btn-outline-light text-black border-black">
                        Update unit
                    </button>

                    <button type="button" onClick={handleDelete}
                        className="btn btn-danger">
                        Delete unit
                    </button>
                </div>
            </ActionBar>
        </div>
    );
}
