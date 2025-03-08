import { Link, useNavigate } from "react-router-dom";
import UnitForm from "../forms/UnitForm";
import { UnitDto } from "../../types/dtos/UnitDto";
import BreadCrumbs from "../layout/Breadcrumbs";
import ActionBar from "../layout/ActionBar";
import { useApiRequest } from "../../hooks/useApiRequest";

export default function NewUnit() {
    const navigate = useNavigate();
    const { handleRequest } = useApiRequest();

    async function handleSubmit(unit: UnitDto) {
        const res = await handleRequest<UnitDto>({
            method: "post",
            url: "/api/units",
            data: unit
        });

        if (res) {
            navigate(`/units/${res.id}`);
        }
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
                <UnitForm initialUnit={null} handleSubmit={handleSubmit} />
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
