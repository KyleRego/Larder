import { useContext, useEffect, useState } from "react";
import { UnitDto } from "../../types/dtos/UnitDto";
import { useNavigate, useParams } from "react-router";
import { apiClient } from "../../util/axios";
import Loading from "../Loading";
import UnitForm from "../forms/UnitForm";
import { MessageContext } from "../../contexts/MessageContext";
import ActionBar from "../layout/ActionBar";
import BreadCrumbs from "../layout/Breadcrumbs";
import { Link } from "react-router-dom";
import { useApiRequest } from "../../hooks/useApiRequest";

export default function EditUnit() {
    const [unit, setUnit] = useState<UnitDto | null>(null);
    const { id } = useParams<{ id: string }>();
    const unitId = id as string;
    const navigate = useNavigate();
    const { setMessage } = useContext(MessageContext);
    const { handleRequest } = useApiRequest();

    useEffect(() => {
        async function getUnit() {
            const res = await handleRequest<UnitDto>({
                method: "get",
                url: `/api/units/${id}`
            });

            if (res) {
                setUnit(res);
            }
        }

        getUnit();
    }, [id]);

    async function handleSubmit(unit: UnitDto) {
        const res = await handleRequest<UnitDto>({
            method: "put",
            url: `/api/units/${unitId}`,
            data: unit
        });

        if (res) {
            navigate(`/units/${unit.id}`);
        }
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
                    <h1 className="fs-6 d-inline">
                        Editing unit    
                    </h1>
                </li>
            </BreadCrumbs>

            <div className="mt-3 container flex-grow-1">
                <UnitForm initialUnit={unit} handleSubmit={handleSubmit} />
            </div>

            <ActionBar>
                <div className="d-flex justify-content-between">
                    <span className="invisible btn">Delete unit</span>
                    <button type="submit" form="unit-form"
                        className="btn btn-outline-light">
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
