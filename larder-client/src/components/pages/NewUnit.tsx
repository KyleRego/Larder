import { Link, useNavigate } from "react-router-dom";
import UnitForm from "../forms/UnitForm";
import { UnitType } from "../../types/dtos/UnitType";
import { apiClient } from "../../util/axios";
import { UnitDto } from "../../types/dtos/UnitDto";
import { ApiResponse } from "../../types/ApiResponse";
import { useContext } from "react";
import { MessageContext } from "../../contexts/MessageContext";
import BreadCrumbs from "../layout/Breadcrumbs";
import ActionBar from "../layout/ActionBar";

export default function NewUnit() {
    const navigate = useNavigate();
    const { setMessage } = useContext(MessageContext);

    function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const formData = new FormData(e.currentTarget);
        const name = formData.get("name") as string;
        const unitType = parseInt(formData.get("type") as string) as UnitType;

        const newUnit = new UnitDto(null, name, unitType, []);

        apiClient.post<ApiResponse<UnitDto>>("/api/units", newUnit).then(res => {
            setMessage({text: res.data.message, type: res.data.type})
            navigate("/units");
        }).catch(error => {
            console.error(error);
        });
    }

    return (
        <div className="d-flex flex-column h-100">
            <BreadCrumbs>
                <li className="breadcrumb-item" aria-current="page">
                    <Link to={"/units"}>Units</Link>
                </li>
                <li className="breadcrumb-item active">
                    <h1 className="fs-6 d-inline">
                        New unit
                    </h1>
                </li>
            </BreadCrumbs>

            <div className="my-4 flex-grow-1 container">
                <UnitForm unit={null} handleSubmit={handleSubmit} />
            </div>

            <ActionBar>
                <div className="d-flex justify-content-center">
                    <button type="submit" form="unit-form"
                        className="btn btn-outline-light">
                        Create unit
                    </button>
                </div>
            </ActionBar>
        </div>
    );
}
